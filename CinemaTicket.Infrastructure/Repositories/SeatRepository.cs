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
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository(CinemaTicketContext context) : base(context)
        {
        }

        public async Task<bool> UpdateSeatStatusAsync(int seatNumber, int showtimeId)
        {
            var seat = await _dbSet.Where(s => s.SeatNumber == seatNumber && s.ShowtimeId == showtimeId).SingleOrDefaultAsync();

            if (seat == null)
            {
                return false;
            }

            seat.IsReserved = !seat.IsReserved;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
