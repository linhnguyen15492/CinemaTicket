namespace CinemaTicket.Core.Dtos
{
    public class TicketBookingDto : BaseDto
    {
        public int ShowtimeId { get; set; }
        public int BookingStatusId { get; set; }
        public string BookingStatus { get; set; } = string.Empty;
        public double Price { get; set; }
        public int ScreeningRoomId { get; set; }
        public string ScreeningRoomName { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public int TheaterId { get; set; }
        public string TheaterName { get; set; } = string.Empty;

        public List<TicketBookingDetailDto> TicketBookingDetails { get; set; } = new List<TicketBookingDetailDto>();
    }

    public class CreateTicketDto : IDto
    {
        public int ShowtimeId { get; set; }
        public int BookingStatusId { get; set; }
        public double Price { get; set; }
        public List<TicketBookingDetailDto> TicketBookingDetails { get; set; } = new List<TicketBookingDetailDto>();
    }
}
