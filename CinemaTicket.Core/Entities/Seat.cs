using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class Seat
    {
        public int SeatNumber { get; set; }

        public int ShowtimeId { get; set; }

        [ForeignKey("ShowtimeId")]
        public Showtime Showtime { get; set; } = default!;

        public bool IsReserved { get; set; } = false;
    }
}
