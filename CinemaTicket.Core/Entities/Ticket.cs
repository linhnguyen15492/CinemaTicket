using CinemaTicket.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public int ShowtimeId { get; set; }
        public ICollection<TicketDetail> TicketDetails { get; set; } = default!;
    }
}
