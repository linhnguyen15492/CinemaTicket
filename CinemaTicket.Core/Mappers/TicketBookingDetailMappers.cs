using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Core.Mappers
{
    public static class TicketBookingDetailMappers
    {
        public static TicketBookingDetailDto ToTicketBookingDetailDto(this TicketBookingDetail ticketBookingDetail)
        {
            return new TicketBookingDetailDto
            {
                Id = ticketBookingDetail.Id,
                TicketBookingId = ticketBookingDetail.TicketBookingId,
                CinemaSeatId = ticketBookingDetail.CinemaSeatId
            };
        }
    }
}
