using CinemaTicket.Core.Entities;

namespace CinemaTicket.Core.Dtos
{
    public class TicketDto : BaseDto
    {
        public int ShowtimeId { get; set; }
        public int ScreeningRoomId { get; set; }
        public string ScreeningRoomName { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public int TheaterId { get; set; }
        public string TheaterName { get; set; } = string.Empty;
    }

    public class CreateTicketDto : IDto
    {
        public int ShowtimeId { get; set; }

        public ICollection<TicketDetailDto> TicketDetails { get; set; } = default!;
    }

    public class ResponseTicketDto : IDto
    {
        public int ShowtimeId { get; set; }

        public int TicketId { get; set; }
    }
}
