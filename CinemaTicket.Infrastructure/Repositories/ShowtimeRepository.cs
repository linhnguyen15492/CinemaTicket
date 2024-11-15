using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Infrastructure.Context;
using CinemaTicket.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class ShowtimeRepository : GenericRepository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(CinemaTicketContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Showtime>> GetAllAsync()
        {
            return await _dbSet.Include(s => s.Movie)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s!.Theater)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s!.ScreeningRoomType)
                                .Include(s => s.ShowtimeSchedule)
                                .ToListAsync();
        }

        public override async Task<Showtime?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(s => s.Id == id)
                                .Include(s => s.Movie)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s!.Theater)
                                .Include(s => s.ScreeningRoom)
                                    .ThenInclude(s => s!.ScreeningRoomType)
                                .Include(s => s.ShowtimeSchedule)
                                .FirstOrDefaultAsync();
        }
    }
}
