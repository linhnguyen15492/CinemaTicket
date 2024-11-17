using CinemaTicket.Core.Enums;
using CinemaTicket.Web.Dtos;
using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using CinemaTicket.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Web.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly ShowtimeService _showtimeService;
        private readonly MovieService _movieService;
        private readonly TheaterService _theaterService;

        public ShowtimesController(ShowtimeService showtimeService, MovieService movieService, TheaterService theaterService)
        {
            _showtimeService = showtimeService;
            _movieService = movieService;
            _theaterService = theaterService;
        }

        public async Task<IActionResult> Index([FromQuery] DateOnly? searchDate, [FromQuery] string? searchName, [FromQuery] int? scheduleType)
        {
            ViewBag.ScheduleTypes = GetScheduleTypes();

            var showtimes = await _showtimeService.GetAllAsync();

            GetScheduleTypes();

            if (!string.IsNullOrEmpty(searchName))
            {
                showtimes = showtimes!.Where(s => s.MovieTitle!.ToUpper().Contains(searchName.ToUpper()));
            }

            if (searchDate is not null)
            {
                showtimes = showtimes!.Where(x => x.Date == searchDate);
            }

            if (scheduleType is not null)
            {
                showtimes = showtimes!.Where(x => x.ShowtimeScheduleId == scheduleType);
            }

            return View(showtimes);
        }


        public async Task<IActionResult> ChooseTheater()
        {
            var theaters = await _theaterService.GetAllAsync();

            ViewData["Theaters"] = new SelectList(theaters, "Id", "Name");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Create([FromQuery] int theaterId)
        {
            var movieList = await _movieService.GetAllAsync();

            var theaters = await _theaterService.GetAllAsync();

            var theater = theaters!.Where(t => t.Id == theaterId).FirstOrDefault();

            if (theater is null)
            {
                return NotFound();
            }

            var rooms = theater.ScreeningRooms;

            var vm = new CreateShowtimeViewModel()
            {
                TheaterName = theater.Name,
                Movies = new SelectList(movieList, "Id", "Title"),
                ShowtimeSchedules = new SelectList(GetScheduleTypes(), "Value", "Text")
            };

            if (rooms!.Count == 0)
            {
                var items = new List<string> { "Chưa có phòng chiếu trong rạp này" };
                vm.ScreeningRooms = new SelectList(items);
            }
            else
            {
                vm.ScreeningRooms = new SelectList(rooms, "Id", "Name");
            }

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateShowtimeDto CreateShowtimeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _showtimeService.AddAsync(CreateShowtimeDto);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(CreateShowtimeDto);
        }

        private IEnumerable<ScheduleType> GetScheduleTypes()
        {
            var scheduleTypes = new List<ScheduleType>();

            foreach (var type in Enum.GetValues(typeof(ShowtimeScheduleEnum)))
            {
                scheduleTypes.Add(new ScheduleType
                {
                    Value = (int)type,
                    Text = type.ToString()!
                });
            }

            return scheduleTypes;
        }
    }
}
