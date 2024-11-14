namespace CinemaTicket.Core.Dtos
{
    public class TheaterDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int ManagerId { get; set; }
        public List<ScreeningRoomDto>? ScreeningRooms { get; set; }
    }

    public class CreateTheaterDto : IDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
    }
}
