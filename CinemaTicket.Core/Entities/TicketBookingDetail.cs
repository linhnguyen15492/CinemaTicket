using CinemaTicket.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities
{
    public class TicketBookingDetail : BaseEntity
    {
        public int TicketBookingId { get; set; }

        [ForeignKey("TicketBookingId")]
        public TicketBooking TicketBooking { get; set; } = default!;

        public int CinemaSeatId { get; set; }

        [ForeignKey("CinemaSeatId")]
        public CinemaSeat CinemaSeat { get; set; } = default!;
    }
}
