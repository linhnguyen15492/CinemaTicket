using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace CinemaTicket.Web.Services
{
    public class TicketService : BaseService, ITicketService
    {
        private readonly UserService _userService;

        public TicketService(string baseUrl, UserService userService)
        {
            BaseUrl = baseUrl;
            _userService = userService;
        }

        public async Task<TicketResponse?> AddAsync(Ticket ticket)
        {
            var token = _userService.GetCurrentToken();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using HttpResponseMessage response = await client.PostAsJsonAsync("tickets/", ticket);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<TicketResponse>();

                return res;
            }

            return null;
        }


        public async Task<IEnumerable<SalesByMovie>?> GetSalesByMovieAsync()
        {
            var token = _userService.GetCurrentToken();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using HttpResponseMessage response = await client.GetAsync("tickets/sales-by-movie");

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<IEnumerable<SalesByMovie>>();

                return res;
            }

            return null;
        }

    }
}
