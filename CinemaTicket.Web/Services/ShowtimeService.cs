using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Services
{
    public class ShowtimeService : BaseService, IShowtimeService
    {
        public ShowtimeService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
        public async Task<IEnumerable<Showtime>?> GetAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync("showtimes");
            if (response.IsSuccessStatusCode)
            {
                var showtimes = await response.Content.ReadFromJsonAsync<IEnumerable<Showtime>>();
                return showtimes;
            }
            else
            {
                return null;
            }
        }
    }
}
