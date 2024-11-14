using CinemaTicket.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;

        public string JwtId { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiresDate { get; set; }
    }
}
