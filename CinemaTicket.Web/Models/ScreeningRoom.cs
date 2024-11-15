namespace CinemaTicket.Web.Models
{
    public class ScreeningRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int ScreeningRoomTypeId { get; set; }
        public string ScreeningRoomType { get; set; } = string.Empty;
        public int TheaterId { get; set; }
        public string Theater { get; set; } = string.Empty;
    }
}
