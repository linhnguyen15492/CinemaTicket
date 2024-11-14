using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Core.Entities
{
    public class Theater : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<ScreeningRoom>? ScreeningRooms { get; set; }
        public ApplicationUser? Manger { get; set; }
    }
}
