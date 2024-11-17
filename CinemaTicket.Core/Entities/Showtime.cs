using CinemaTicket.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class Showtime : BaseEntity
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int SccreeningRoomId { get; set; }

        [ForeignKey("SccreeningRoomId")]
        [Required]
        public ScreeningRoom? ScreeningRoom { get; set; }

        [Required]
        public ShowtimeScheduleEnum ShowtimeScheduleId { get; set; }

        [ForeignKey("ShowtimeScheduleId")]
        [Required]
        public ShowtimeSchedule ShowtimeSchedule { get; set; } = default!;

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        [Required]
        public Movie Movie { get; set; } = default!;

        public bool IsAvailable
        {
            get
            {
                if (TicketDetails?.Count == ScreeningRoom?.Capacity)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            private set { }
        }

        public double Price { get; set; }

        public ICollection<TicketDetail>? TicketDetails { get; set; }

        public ICollection<Seat> Seats { get; set; } = default!;
    }
}
