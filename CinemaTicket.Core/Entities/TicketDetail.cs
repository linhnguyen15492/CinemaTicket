using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Entities
{
    public class TicketDetail : BaseEntity
    {
        public int? TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; } = default!;

        public int ShowtimeId { get; set; }

        public Showtime Showtime { get; set; } = default!;

        public int SeatNumber { get; set; }

        public Seat Seat { get; set; } = default!;

        public double Price { get; set; }
    }
}

