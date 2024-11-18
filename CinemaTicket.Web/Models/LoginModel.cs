using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.Web.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
