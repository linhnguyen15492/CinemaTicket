using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Services
{
    public class SalesService : ISalesService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Showtime> _showtimeRepository;

        public SalesService(IRepository<Ticket> ticketRepository, IRepository<Showtime> showtimeRepository)
        {
            _ticketRepository = ticketRepository;
            _showtimeRepository = showtimeRepository;
        }

        public async Task<object> GetSalesByShow()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            var salesByShow = tickets.Select(t => new
            {
                ShowtimeId = t.ShowtimeId,
                Sales = t.TicketDetails.Sum(td => td.Price),
                Count = t.TicketDetails.Count
            }).GroupBy(c => c.ShowtimeId)
            .Select(g => new
            {
                ShowtimeId = g.Key,
                Sales = g.Sum(x => x.Sales),
                Count = g.Sum(x => x.Count)
            });

            return salesByShow;

        }

        public async Task<object> GetSalesByMovie()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            var showtimes = await _showtimeRepository.GetAllAsync();

            var salesByMovie = tickets.Select(t => new
            {
                Movie = showtimes.FirstOrDefault(s => s.Id == t.ShowtimeId)?.Movie.Title,
                Sales = t.TicketDetails.Sum(td => td.Price),
                Count = t.TicketDetails.Count
            }).GroupBy(c => c.Movie).Select(g => new
            {
                Movie = g.Key,
                Sales = g.Sum(x => x.Sales),
                Count = g.Sum(x => x.Count)
            });

            return salesByMovie;

        }
    }
}
