using App.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public MovieStatusEnum StatusId { get; set; }
        [ForeignKey("StatusId")]
        public MovieStatus Status { get; set; } = default!;
    }
}
