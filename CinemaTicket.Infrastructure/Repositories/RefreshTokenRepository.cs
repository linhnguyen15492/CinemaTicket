using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRepository<RefreshToken>
    {
        public Task<RefreshToken> AddAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>> AddRangeAsync(IEnumerable<RefreshToken> data)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RefreshToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RefreshToken> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshToken> Search(Expression<Func<RefreshToken, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> UpdateAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> UpdateAsync(int id, RefreshToken entity)
        {
            throw new NotImplementedException();
        }
    }
}
