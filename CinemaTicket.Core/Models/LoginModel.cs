using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Core.Models
{
    public class LoginModel
    {

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
