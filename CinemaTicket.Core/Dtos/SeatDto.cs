using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Dtos
{
    public class SeatDto
    {
        public int SeatNumber { get; set; }

        public int ShowtimeId { get; set; }

        public bool IsReserved { get; set; } = false;
    }
}
