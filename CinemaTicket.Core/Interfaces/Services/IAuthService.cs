using CinemaTicket.Core.Models;
using CinemaTicket.Core.Shared;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface IAuthService<T> where T : class
    {
        //Task<Response<IdentityResult>> RegisterSystemUser(SystemRegisterUserDTO user);
        //Task<Response<LoginResponseDTO>> LoginSystemUser(SystemSignInUserDTO credentials);

        Task<Result<T>> RegisterAsync(RegisterModel user);
        Task<Result<TokenModel>> LoginAsync(LoginModel loginModel);
        Task<Result<TokenModel>> LoginAsync(string username, string password);
    }
}
