using CinemaTicket.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public int ShowtimeId { get; set; }

        public int Quantity
        {
            get
            {
                return TicketDetails.Count;
            }

            private set { }
        }

        public double Total
        {
            get
            {
                return TicketDetails.Count * Price;
            }

            private set { }
        }

        public double Price { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; } = default!;
    }
}
