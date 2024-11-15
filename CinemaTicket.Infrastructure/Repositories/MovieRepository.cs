using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Infrastructure.Context;
using CinemaTicket.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaTicketContext context) : base(context) { }

        public override async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbSet.Include(m => m.Status).ToListAsync();
        }
    }
}
