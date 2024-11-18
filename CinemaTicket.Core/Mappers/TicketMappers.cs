using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Core.Mappers
{
    public static class TicketMappers
    {
        public static TicketDto ToTicketDto(this Ticket ticket)
        {
            return new TicketDto
            {
                Id = ticket.Id,
                ShowtimeId = ticket.ShowtimeId,
            };
        }

        public static ResponseTicketDto ToResponseTicketDto(this Ticket ticket)
        {
            return new ResponseTicketDto
            {
                TicketId = ticket.Id,
                ShowtimeId = ticket.ShowtimeId,
            };
        }

        public static TicketDetail ToTicketDetail(this TicketDetailDto ticketDetailDto)
        {
            return new TicketDetail
            {
                ShowtimeId = ticketDetailDto.ShowtimeId,
                SeatNumber = ticketDetailDto.SeatNumber,
                Price = ticketDetailDto.Price,
            };
        }


    }
}
