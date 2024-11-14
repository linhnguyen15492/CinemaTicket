using App.Core.Domain.Dtos;
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
    public class ShowtimeRepository : GenericRepository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(DbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Showtime>> GetAllAsync()
        {
            return await _dbSet.Include(s => s.Movie)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s.Theater)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s.ScreeningRoomType)
                                .Include(s => s.ShowtimeSchedule)
                                .ToListAsync();
        }

        public override async Task<Showtime?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(s => s.Id == id)
                                .Include(s => s.Movie)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s.Theater)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s.ScreeningRoomType)
                                .Include(s => s.ShowtimeSchedule)
                                .FirstOrDefaultAsync();
        }
    }
}
