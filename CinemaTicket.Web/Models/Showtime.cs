using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Web.Models
{
    public class Showtime : BaseEntity
    {
        [Display(Name = "Ngày")]
        public DateOnly Date { get; set; }

        public int SccreeningRoomId { get; set; }

        [Display(Name = "Loại phòng")]
        public string ScreeningRoomType { get; set; } = string.Empty;

        public int ShowtimeScheduleId { get; set; }

        public string ShowtimeSchedule { get; set; } = string.Empty;

        public int MovieId { get; set; }

        [Display(Name = "Tên phim")]
        public string MovieTitle { get; set; } = string.Empty;

        [Display(Name = "Giá vé")]
        public double Price { get; set; }

        public int TheaterId { get; set; }

        [Display(Name = "Tên rạp")]
        public string TheaterName { get; set; } = string.Empty;
    }
}
