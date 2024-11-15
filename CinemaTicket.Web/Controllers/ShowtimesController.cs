using CinemaTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Web.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly ShowtimeService _service;

        public ShowtimesController(ShowtimeService service)
        {
            _service=service;
        }

        public async Task<IActionResult> Index()
        {
            var showtimes = await _service.GetAllAsync();

            return View(showtimes);
        }
    }
}
