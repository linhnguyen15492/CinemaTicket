using CinemaTicket.Web.Enums;

namespace CinemaTicket.Web.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int ShowtimeId { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; } = default!;
    }

    public class TicketResponse
    {
        public int TicketId { get; set; }
        public int ShowtimeId { get; set; }
    }
}
