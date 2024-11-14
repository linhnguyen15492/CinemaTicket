namespace CinemaTicket.Core.Dtos
{
    public class ScreeningRoomDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int ScreeningRoomTypeId { get; set; }
        public string ScreeningRoomType { get; set; } = string.Empty;
        public int TheaterId { get; set; }
        public string Theater { get; set; } = string.Empty;
    }

    public class CreateScreeningRoomDto : IDto
    {
        public string Name { get; set; } = string.Empty;
        public int ScreeningRoomTypeId { get; set; }
        public int TheaterId { get; set; }
    }
}
