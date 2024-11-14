using CinemaTicket.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicket.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(string email, string password);
        Task<IdentityResult> SignInAsync(string email, string password);
        Task<IdentityResult> SignUpAsync(RegisterModel model);
        Task<IdentityResult> SignInAsync(LoginModel model);
    }
}
