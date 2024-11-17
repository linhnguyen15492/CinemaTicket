using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Core.Dtos
{
    public class ShowtimeDto : BaseDto
    {
        public DateOnly Date { get; set; }
        public int SccreeningRoomId { get; set; }
        public string ScreeningRoomType { get; set; } = string.Empty;

        public int ShowtimeScheduleId { get; set; }

        public string ShowtimeSchedule { get; set; } = string.Empty;
        public int MovieId { get; set; }

        public string MovieTitle { get; set; } = string.Empty;

        public double Price { get; set; }

        public int TheaterId { get; set; }

        public string TheaterName { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public ICollection<SeatDto>? Seats { get; set; }
    }

    public class CreateShowtimeDto : IDto
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int SccreeningRoomId { get; set; }

        [Required]
        public int ShowtimeScheduleId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public double Price { get; set; }
    }
}
