using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CinemaTicket.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _service;

        public MoviesController(MovieService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync();

            return View(movies);
        }

        //GET: Movies/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _service.

        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        public async Task<IEnumerable<Movie>?> GetAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5073/api/");

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
    }
}
