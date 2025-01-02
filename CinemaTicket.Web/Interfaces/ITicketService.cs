using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface ITicketService
    {
        Task<TicketResponse?> AddAsync(Ticket ticket);
        Task<IEnumerable<SalesByMovie>?> GetSalesByMovieAsync();
    }
}