namespace CinemaTicket.Core.Entities
{
    public class CinemaSeat : BaseEntity
    {
        public bool IsAvailable { get; set; } = true;
        public int ScreeningRoomId { get; set; }
        public ScreeningRoom ScreeningRoom { get; set; } = default!;
        public ICollection<TicketBookingDetail> TicketBookingsDetails { get; set; } = new List<TicketBookingDetail>();
    }
}
