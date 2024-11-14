using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Infrastructure.Context;
using CinemaTicket.Infrastructure.Services.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ISeedDataService _seeder;
        private readonly CinemaTicketContext _context;

        public HomeController(CinemaTicketContext context, ISeedDataService seeder, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _seeder = new SeedDataService(context, roleManager, userManager);
            _context = context;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            try
            {
                await _seeder.SeedDataAsync();

                return Ok(_seeder.Messages);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-database")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDatabaseAsync()
        {
            var res = await _context.Database.EnsureCreatedAsync();

            if (res)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }

        }
    }
}
