using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums;

namespace CinemaTicket.Core.Mappers
{
    public static class ScreeningRoomMappers
    {
        public static ScreeningRoomDto ToScreeningRoomDto(this ScreeningRoom screeningRoom)
        {
            return new ScreeningRoomDto
            {
                Id = screeningRoom.Id,
                Name = screeningRoom.Name,
                Capacity = screeningRoom.Capacity,
                ScreeningRoomTypeId = (int)screeningRoom.ScreeningRoomTypeId,
                ScreeningRoomType = screeningRoom.ScreeningRoomType.Description,
                TheaterId = screeningRoom.TheaterId,
                Theater = screeningRoom.Theater.Name
            };
        }

        public static ScreeningRoom ToScreeningRoom(this CreateScreeningRoomDto createScreeningRoomDto)
        {
            return new ScreeningRoom
            {
                Name = createScreeningRoomDto.Name,
                ScreeningRoomTypeId = (ScreeningRoomTypeEnum)createScreeningRoomDto.ScreeningRoomTypeId,
                TheaterId = createScreeningRoomDto.TheaterId,
                CreatedDate = DateTime.UtcNow,
            };
        }
    }
}
