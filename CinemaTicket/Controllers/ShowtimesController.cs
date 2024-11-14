using App.Core.Interfaces.Services;
using App.Core.Shared;
using App.Infrastructure.MySQL.Dtos;
using App.Infrastructure.MySQL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace App.API.Controllers
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
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _service.GetAllAsync();

            if (res.IsSuccess)
            {
                return Ok(ApiResponse<IEnumerable<IEntityDto>>.Success(res.Value!));
            }
            else
            {
                return NotFound(ApiResponse<IEnumerable<IEntityDto>>.Failure("No showtime found!"));
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

                return BadRequest(ApiResponse<IEntityDto>.Failure(errors.ToArray()));
            }

            var result = await _service.CreateAsync(createShowtimeDto);

            if (result.IsSuccess)
            {
                return Ok(ApiResponse<IEntityDto>.Success(result.Value!));
            }
            else
            {
                return BadRequest(ApiResponse<IEntityDto>.Failure(result.Errors!));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var res = await _service.GetByIdAsync(id);

            if (res.IsSuccess)
            {
                return Ok(ApiResponse<IEntityDto>.Success(res.Value!));
            }
            else
            {
                return NotFound(ApiResponse<IEntityDto>.Failure("Not found"));
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
                return Ok(ApiResponse<IEnumerable<IEntityDto>>.Success(res.Value!));
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
