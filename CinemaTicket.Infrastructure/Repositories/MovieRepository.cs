using App.Core.Entities;
using App.Core.Interfaces.Repositories;
using App.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context) { }

        public override async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbSet.Include(m => m.Status).ToListAsync();
        }
    }
}
