namespace CinemaTicket.Web.Models
{
    public class Theater : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int ManagerId { get; set; }
        public List<ScreeningRoom>? ScreeningRooms { get; set; }
    }
}
