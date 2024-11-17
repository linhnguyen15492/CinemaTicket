using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.ViewModels
{
    public class TicketViewModel : BaseViewModel
    {
        public int ShowtimeId { get; set; }
        public int SeatNumber { get; set; }

        public Showtime Showtime { get; set; } = default!;

    }
}
