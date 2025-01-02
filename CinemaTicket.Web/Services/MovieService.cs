using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Services
{
    public class MovieService : BaseService, IMovieService
    {

        public MovieService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<IEnumerable<Movie>?> GetAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync("movies");
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadFromJsonAsync<IEnumerable<Movie>>();
                return movies;
            }
            else
            {
                return null;
            }
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync($"movies/{id}");
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadFromJsonAsync<Movie>();
                return movie;
            }
            else
            {
                return null;
            }
        }
    }
}
