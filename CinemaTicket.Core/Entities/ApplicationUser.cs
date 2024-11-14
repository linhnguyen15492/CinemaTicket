using App.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string EmployeeCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        bool IsDeleted { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Theater>? Theaters { get; set; } = new List<Theater>();

        public DepartmentEnum DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = default!;

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
