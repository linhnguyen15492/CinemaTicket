using CinemaTicket.Web.Dtos;
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

        public async Task<Showtime?> AddAsync(CreateShowtimeDto createShowtimeDto)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            using HttpResponseMessage response = await client.PostAsJsonAsync("showtimes/create-showtime", createShowtimeDto);

            if (response.IsSuccessStatusCode)
            {
                var showtime = await response.Content.ReadFromJsonAsync<Showtime>();
                return showtime;
            }
            else
            {
                return null;
            }
        }


        public async Task<Showtime?> GetShowtimeByIdAsync(int showtimeId)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync($"showtimes/{showtimeId}");
            if (response.IsSuccessStatusCode)
            {
                var showtime = await response.Content.ReadFromJsonAsync<Showtime>();
                return showtime;
            }
            else
            {
                return null;
            }
        }

    }
}
