using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Web.Models
{
    public class Movie : BaseEntity
    {
        [Display(Name = "Tên phim")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Thể loại")]
        public string Genre { get; set; } = string.Empty;

        public int StatusId { get; set; }

        [Display(Name = "Tình trạng")]
        public string? Status { get; set; } = string.Empty;
    }
}
