using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Web.Models
{
    public class TicketDetail
    {
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; } = default!;

        public int ShowtimeId { get; set; }

        public Showtime Showtime { get; set; } = default!;

        public int SeatNumber { get; set; }

        public Seat Seat { get; set; } = default!;

        public double Price { get; set; }
    }
}
