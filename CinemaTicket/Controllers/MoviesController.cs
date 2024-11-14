using Microsoft.AspNetCore.Mvc;
using App.Core.Interfaces.Services;
using App.Core.Shared;
using App.Infrastructure.MySQL.Dtos;

namespace App.API.Controllers
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
                return Ok(ApiResponse<IEnumerable<IEntityDto>>.Success(res.Value!, "Get all movies success"));
            }
            else
            {
                return BadRequest(ApiResponse<IEnumerable<IEntityDto>>.Failure(res.Errors!));
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
                return Ok(ApiResponse<IEntityDto>.Success(res.Value!, "Get movie by id success"));
            }
            else
            {
                return NotFound(ApiResponse<IEntityDto>.Failure(res.Errors!));
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
                return Ok(ApiResponse<IEntityDto>.Success(result.Value!, "Create movies success"));
            }
            else
            {
                return BadRequest(ApiResponse<IEntityDto>.Failure(result.Errors!));
            }
        }
    }
}
