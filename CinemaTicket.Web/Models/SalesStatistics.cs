namespace CinemaTicket.Web.Models
{
    public class SalesStatistics
    {

    }

    public class SalesByMovie
    {
        public string Movie { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public int Count { get; set; }
    }
}
