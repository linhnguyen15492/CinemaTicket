using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface ICartService
    {
        void ClearCart();
        Ticket? GetCart();
        void SaveCartSession(Ticket ticket);
    }
}