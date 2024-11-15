using Microsoft.AspNetCore.Mvc;
using CinemaTicket.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Shared;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly ITheaterService _service;
        private readonly IRepository<Theater> _repository;

        public TheatersController(ITheaterService service, IRepository<Theater> repository)
        {
            _service = service;
            _repository = repository;
        }

        // GET: api/Theaters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors!);
            }
            else
            {
                return Ok((IEnumerable<TheaterDto>)result.Value!);
            }
        }

        // GET: api/Theaters/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
            {

                return NotFound(ApiResponse<IDto>.Failure(result.Errors!));
            }
            else
            {
                return Ok(ApiResponse<IDto>.Success(result.Value!));
            }

        }

        [HttpPost]
        [Route("create-theater")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTheaterDto theater)
        {
            var result = await _service.CreateAsync(theater);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IDto>.Failure(result.Errors!));
            }
            else
            {
                var res = ApiResponse<IDto>.Success(result.Value!);
                res.Messages.Add($"Item created successfully!");
                return Ok(res);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator, OfficeManager")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IDto>.Failure(result.Errors!));
            }
            else
            {
                return Ok(ApiResponse<IDto>.Success(result.Value!));
            }
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator, OfficeManager")]
        public async Task<IActionResult> UpdateAsync([FromBody] TheaterDto theaterDto)
        {
            var result = await _service.UpdateAsync(theaterDto);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IDto>.Failure(result.Errors!));
            }
            else
            {
                return Ok(ApiResponse<IDto>.Success(result.Value!));
            }
        }

        [HttpPost]
        [Route("create-screening-room")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.OfficeManager}")]
        public async Task<IActionResult> CreateScreeningRoomAsync([FromBody] CreateScreeningRoomDto screeningRoomDto)
        {
            var result = await _service.CreateScreeningRoomAsync(screeningRoomDto);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IDto>.Failure(result.Errors!));
            }
            else
            {
                return Ok(ApiResponse<IDto>.Success(result.Value!));
            }
        }
    }
}
