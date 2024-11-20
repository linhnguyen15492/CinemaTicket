using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie == null)
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Có lỗi xảy ra!"
                };

                return View("Error", vm);
            }
            else
            {
                return View(movie);
            }
        }
    }
}
