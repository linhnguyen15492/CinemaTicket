using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Models;
using CinemaTicket.Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService<ApplicationUserDto> _service;

        public AccountController(IAccountService<ApplicationUserDto> accountService)
        {
            _service = accountService;
        }


        [Route("validateUser")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Validate(LoginModel model)
        {
            var result = await _service.LoginAsync(model.Username, model.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<TokenModel>.Failure("Invalid Username or Password"));
            }
            else
            {
                var message = "Login Successful";
                return BadRequest(ApiResponse<TokenModel>.Success(result.Value!, message));
            };
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var result = await _service.LoginAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result.Value!);
            }
            else
            {
                return NotFound(result.Errors!);
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _service.RegisterAsync(model);

            if (result.IsSuccess)
            {
                string messages = "Register Successful";
                return Ok(ApiResponse<ApplicationUserDto>.Success(result.Value!, messages));
            }
            else
            {
                return BadRequest(ApiResponse<ApplicationUserDto>.Failure(result.Errors!));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                var response = new ObjectResult(result.Value);
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            else
            {
                var response = new ObjectResult(result.Value);
                response.StatusCode = StatusCodes.Status404NotFound;
                return response;
            }
        }
    }
}