using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaTicket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, DatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var info = await GetDatabaseInfo();

            if (info is not null)
            {
                return View(info);
            }
            else
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Chưa có kết nối tới server, vui lòng khởi chạy Web API"
                };

                return View("Error", vm);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CreateDatabase()
        {
            var result = await _databaseService.CreateDatabseAsync();

            if (result)
            {
                { return RedirectToAction("Index"); }
            }
            else
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Database creation failed"
                };

                return RedirectToAction("Error", vm);
            }

        }

        public async Task<IActionResult> DropDatabase()
        {
            var result = await _databaseService.DropDatabseAsync();

            if (result)
            {
                { return RedirectToAction("Index"); }
            }
            else
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Failed to drop database"
                };

                return RedirectToAction("Error", vm);
            }

        }

        public async Task<IActionResult> SeedData()
        {
            var result = await _databaseService.SeedDataAsync();

            if (result)
            {
                { return RedirectToAction("Index"); }
            }
            else
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Failed to drop database"
                };

                return RedirectToAction("Error", vm);
            }
        }

        private async Task<DatabaseInfo?> GetDatabaseInfo()
        {
            return await _databaseService.GetDatabaseInfo();
        }
    }
}
