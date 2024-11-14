using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Web.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
