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
                Price = ticket.Price,
            };
        }
    }
}
