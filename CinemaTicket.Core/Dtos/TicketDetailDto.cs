using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Dtos
{
    public class TicketDetailDto
    {
        public int ShowtimeId { get; set; }
        public int SeatNumber { get; set; }
        public double Price { get; set; }
    }
}
