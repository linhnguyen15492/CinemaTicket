using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Mappers;
using CinemaTicket.Core.Shared;
using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CinemaTicket.Core.Configurations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CinemaTicket.Infrastructure.Services
{
    public class AccountService : IAccountService<ApplicationUserDto>
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;


        public AccountService(IRepository<ApplicationUser> repository, IRepository<RefreshToken> refreshTokenRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtOptions> options)
        {
            _repository = repository;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtOptions = options.CurrentValue;
        }

        private async Task<Tuple<string, string>> GenerateToken(ApplicationUser user)
        {
            var secretKey = _jwtOptions.SecretKey;

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", user.UserName!),
                    new Claim("Id", user.Id!),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),

                    // roles                   
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            // lưu xuống database
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id!,
                JwtId = token.Id!,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                CreatedDate = DateTime.UtcNow,
                ExpiresDate = DateTime.UtcNow.AddHours(1),
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return Tuple.Create(accessToken, refreshToken);
        }

        private Tuple<string, string> GenerateJwtToken(ApplicationUser user)
        {
            var secretKey = _jwtOptions.SecretKey;
            var key = Encoding.ASCII.GetBytes(secretKey);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));


            return Tuple.Create(new JwtSecurityTokenHandler().WriteToken(token), GenerateRefreshToken());
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ApplicationUser? Validate(string username, string password)
        {
            var user = _repository.GetAll().SingleOrDefault(u => u.UserName == username && u.PasswordHash == password);

            if (user is not null)
            {
                return user;
            }

            return null;
        }

        private async Task<ApplicationUser?> Validate(LoginModel model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Result<ApplicationUserDto>> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                CreatedDate = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Result<ApplicationUserDto>.Success(user.ToUserDto());
            }
            else
            {
                return Result<ApplicationUserDto>.Failure("Register failed");
            }
        }

        public async Task<Result<TokenModel>> LoginAsync(LoginModel loginModel)
        {
            var user = await Validate(loginModel);

            if (user is null)
            {
                return Result<TokenModel>.Failure("Invalid username or password");
            }
            else
            {
                var token = GenerateJwtToken(user);

                return Result<TokenModel>.Success(new TokenModel
                {
                    AccessToken = token.Item1,
                    RefreshToken = token.Item2,
                });
            }
        }

        public async Task<Result<TokenModel>> LoginAsync(string username, string password)
        {
            var user = Validate(username, password);

            if (user is null)
            {
                return await Task.FromResult(Result<TokenModel>.Failure("Invalid username or password"));
            }
            else
            {
                var token = await GenerateToken(user);

                return await Task.FromResult(Result<TokenModel>.Success(new TokenModel
                {
                    AccessToken = token.Item1,
                    RefreshToken = token.Item2
                }));
            }
        }


        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _ = int.TryParse(_jwtOptions.TokenValidityInMinutes.ToString(), out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        public Task<Result<IEnumerable<ApplicationUserDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public Task<Result<ApplicationUserDto>> UpdateAsync(ApplicationUserDto t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ApplicationUserDto>> CreateAsync(ApplicationUserDto t)
        {
            throw new NotImplementedException();
        }

        private async Task<Result<TokenModel>> RefreshToken(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return Result<TokenModel>.Failure("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Result<TokenModel>.Failure("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Result<TokenModel>.Success(new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<Result<ApplicationUserDto?>> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return Result<ApplicationUserDto?>.Failure("User not found");
            }

            return Result<ApplicationUserDto?>.Success(user.ToUserDto());
        }

        public Task<Result<ApplicationUserDto>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ApplicationUserDto>>> SearchAsync(object queryObject)
        {
            throw new NotImplementedException();
        }
    }
}
