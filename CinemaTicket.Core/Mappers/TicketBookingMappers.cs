using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Core.Mappers
{
    public static class TicketBookingMappers
    {
        public static TicketBookingDto ToTicketBookingDto(this TicketBooking ticketBooking)
        {
            return new TicketBookingDto
            {
                Id = ticketBooking.Id,
                ShowtimeId = ticketBooking.ShowtimeId,
                BookingStatusId = (int)ticketBooking.StatusId,
                BookingStatus = ticketBooking.Status.Description,
                Price = ticketBooking.Price,
                ScreeningRoomId = ticketBooking.Showtime.SccreeningRoomId,
                ScreeningRoomName = ticketBooking.Showtime.ScreeningRoom!.Name,
                MovieId = ticketBooking.Showtime.MovieId,
                MovieName = ticketBooking.Showtime.Movie!.Title,
                TheaterId = ticketBooking.Showtime.ScreeningRoom!.TheaterId,
                TheaterName = ticketBooking.Showtime.ScreeningRoom!.Theater!.Name,
                TicketBookingDetails = ticketBooking.TicketBookingDetails.Select(x => x.ToTicketBookingDetailDto()).ToList()
            };
        }
    }
}
