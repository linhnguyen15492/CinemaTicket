using Microsoft.AspNetCore.Mvc;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Shared;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _service.GetAllAsync();
            if (res.IsSuccess)
            {
                return Ok(ApiResponse<IEnumerable<IDto>>.Success(res.Value!, "Get all movies success"));
            }
            else
            {
                return BadRequest(ApiResponse<IEnumerable<IDto>>.Failure(res.Errors!));
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var res = await _service.GetByIdAsync(id);

            if (res.IsSuccess)
            {
                return Ok(ApiResponse<IDto>.Success(res.Value!, "Get movie by id success"));
            }
            else
            {
                return NotFound(ApiResponse<IDto>.Failure(res.Errors!));
            }
        }

        [HttpPost]
        [Route("create-movie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateMovieDto movieDto)
        {
            var result = await _service.CreateAsync(movieDto);

            if (result.IsSuccess)
            {
                return Ok(ApiResponse<IDto>.Success(result.Value!, "Create movies success"));
            }
            else
            {
                return BadRequest(ApiResponse<IDto>.Failure(result.Errors!));
            }
        }
    }
}
