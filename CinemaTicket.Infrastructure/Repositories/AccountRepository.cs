using App.Core.Authentication;
using App.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<IdentityResult> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignInAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignUpAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignUpAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
