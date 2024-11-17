using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Shared;
using CinemaTicket.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace CinemaTicket.Controllers
{
    [Route("api/showtimes")]
    [ApiController]
    public class ShowtimesController : ControllerBase
    {
        private readonly IShowtimeService _service;

        public ShowtimesController(IShowtimeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _service.GetAllAsync();

            if (res.IsSuccess)
            {
                return Ok((IEnumerable<ShowtimeDto>)(res.Value!));
            }
            else
            {
                return NotFound("No showtime found!");
            }
        }

        [HttpPost]
        [Route("create-showtime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateShowtimeDto createShowtimeDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();

                foreach (var modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors.ToArray());
            }

            var result = await _service.CreateAsync(createShowtimeDto);

            if (result.IsSuccess)
            {
                return Ok(result.Value!);
            }
            else
            {
                return BadRequest(result.Errors!);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var res = await _service.GetShowtimeByIdWithSeatsAsync(id);

            if (res.IsSuccess)
            {
                return Ok(res.Value!);
            }
            else
            {
                return NotFound(res.Errors);
            }
        }



        [HttpGet]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchAsync([FromQuery] string? date, [FromQuery] string? movie, [FromQuery] int? scheduleId)
        {

            var request = new ShowtimeQuery
            {
                Date = date,
                Movie = movie,
                ScheduleId = scheduleId
            };

            var res = await _service.SearchAsync(request);

            if (res.IsSuccess)
            {
                return Ok((IEnumerable<ShowtimeDto>)res.Value!);
            }


            return NotFound();
        }

        [HttpGet]
        [Route("query")]
        public IActionResult SearchAsync([FromQuery] ShowtimeQuery query)
        {

            return Content(JsonConvert.SerializeObject(query));
        }
    }

}
