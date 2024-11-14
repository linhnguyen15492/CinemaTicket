using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums;

namespace CinemaTicket.Core.Mappers
{
    public static class ShowtimeMappers
    {
        public static ShowtimeDto ToShowtimeDto(this Showtime showtime)
        {
            return new ShowtimeDto
            {
                Id = showtime.Id,
                Date = showtime.Date,
                SccreeningRoomId = showtime.SccreeningRoomId,
                ScreeningRoomType = showtime.ScreeningRoom?.ScreeningRoomType?.Name!,
                ShowtimeScheduleId = (int)showtime.ShowtimeScheduleId,
                ShowtimeSchedule = showtime.ShowtimeSchedule?.Description!,
                MovieId = showtime.MovieId,
                MovieTitle = showtime.Movie.Title,
                Price = showtime.Price,
            };
        }

        public static Showtime ToShowtime(this ShowtimeDto showtimeDto)
        {
            return new Showtime
            {
                Date = showtimeDto.Date,
                SccreeningRoomId = showtimeDto.SccreeningRoomId,
                ShowtimeScheduleId = (ShowtimeScheduleEnum)showtimeDto.ShowtimeScheduleId,
                MovieId = showtimeDto.MovieId,
                Price = showtimeDto.Price,
                CreatedDate = DateTime.Now,
            };
        }

        public static Showtime ToShowtime(this CreateShowtimeDto showtimeDto)
        {
            return new Showtime
            {
                Date = showtimeDto.Date,
                SccreeningRoomId = showtimeDto.SccreeningRoomId,
                ShowtimeScheduleId = (ShowtimeScheduleEnum)showtimeDto.ShowtimeScheduleId,
                MovieId = showtimeDto.MovieId,
                Price = showtimeDto.Price,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
