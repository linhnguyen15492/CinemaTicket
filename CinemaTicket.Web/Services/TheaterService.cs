using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Services
{
    public class TheaterService : BaseService
    {
        public TheaterService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<IEnumerable<Theater>?> GetAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync("theaters");
            if (response.IsSuccessStatusCode)
            {
                var theaters = await response.Content.ReadFromJsonAsync<IEnumerable<Theater>>();
                return theaters;
            }
            else
            {
                return null;
            }
        }
    }
}
