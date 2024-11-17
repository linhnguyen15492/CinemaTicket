using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Web.Dtos
{
    public class CreateShowtimeDto
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
