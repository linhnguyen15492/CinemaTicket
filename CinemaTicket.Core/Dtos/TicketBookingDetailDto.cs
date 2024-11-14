namespace CinemaTicket.Core.Dtos
{
    public class TicketBookingDetailDto : BaseDto
    {
        public int TicketBookingId { get; set; }
        public int CinemaSeatId { get; set; }
    }
}
