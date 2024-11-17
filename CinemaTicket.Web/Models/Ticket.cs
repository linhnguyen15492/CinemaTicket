using CinemaTicket.Web.Enums;

namespace CinemaTicket.Web.Models
{
    public class Ticket
    {
        public int ShowtimeId { get; set; }

        public double Price { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; } = default!;
    }
}
