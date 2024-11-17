namespace CinemaTicket.Web.Models
{
    public class Seat
    {
        public int SeatNumber { get; set; }

        public int ShowtimeId { get; set; }

        public bool IsReserved { get; set; } = false;
    }
}
