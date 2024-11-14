using App.Core.Enums.EnumClasses;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums;

namespace App.Core.Entities
{
    public class TicketBooking : BaseEntity
    {
        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; } = default!;
        public BookingStatusEnum StatusId { get; set; }
        public BookingStatus Status { get; set; } = default!;
        public double Price { get; set; }
        public int Quantity
        {
            get
            {
                return TicketBookingDetails.Count;
            }

            private set { }
        }
        public ICollection<TicketBookingDetail> TicketBookingDetails { get; set; } = new List<TicketBookingDetail>();
    }
}
