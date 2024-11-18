using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Services
{
    public class TicketService : BaseService
    {

        public TicketService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
        public async Task<TicketResponse?> AddAsync(Ticket ticket)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            using HttpResponseMessage response = await client.PostAsJsonAsync("tickets/", ticket);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<TicketResponse>();

                return res;
            }

            return null;
        }
    }
}
