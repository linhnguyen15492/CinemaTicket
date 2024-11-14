using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Core.Dtos
{
    public class MovieDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string? Status { get; set; } = string.Empty;
    }

    public class CreateMovieDto : IDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;
    }
}
