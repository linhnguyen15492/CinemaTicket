using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class TheaterRepository : GenericRepository<Theater>, ITheaterRepository
    {
        public TheaterRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Theater>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.ScreeningRooms!)
                .ThenInclude(x => x.ScreeningRoomType)
                .ToListAsync();
        }

        public override async Task<Theater?> GetByIdAsync(int id)
        {
            var p = await _dbSet.Where(x => x.Id == id).Include(x => x.ScreeningRooms!)
                .ThenInclude(x => x.ScreeningRoomType)
                .FirstOrDefaultAsync();

            return p;
        }
    }
}
