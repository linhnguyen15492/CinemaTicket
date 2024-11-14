using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;

namespace CinemaTicket.Core.Mappers
{
    public static class TheaterMappers
    {
        public static TheaterDto ToTheaterDto(this Theater theater)
        {
            return new TheaterDto
            {
                Id = theater.Id,
                Name = theater.Name,
                Location = theater.Location,
                ScreeningRooms = theater.ScreeningRooms?.Select(x => x.ToScreeningRoomDto()).ToList()
            };
        }

        public static Theater ToTheater(this CreateTheaterDto createTheaterDto)
        {
            return new Theater
            {
                Name = createTheaterDto.Name,
                Location = createTheaterDto.Location
            };
        }
    }
}
