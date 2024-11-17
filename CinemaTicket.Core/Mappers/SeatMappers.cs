using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Mappers
{
    public static class SeatMappers
    {
        public static SeatDto ToSeatDto(this Seat seat)
        {
            return new SeatDto
            {
                SeatNumber = seat.SeatNumber,
                ShowtimeId = seat.ShowtimeId,
                IsReserved = seat.IsReserved,
            };
        }
    }
}
