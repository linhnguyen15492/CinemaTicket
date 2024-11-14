using App.Core.Interfaces.Services;
using App.Core.Shared;
using App.Infrastructure.MySQL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService=ticketService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _ticketService.GetAllAsync();
            if (res.IsSuccess)
            {

                return Ok(ApiResponse<IEnumerable<IEntityDto>>.Success(res.Value!));
            }
            else
            {
                return BadRequest(ApiResponse<IEnumerable<IEntityDto>>.Failure(res.Errors));
            }
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] CreateTicketDto createTicketDto)
        {


            return Ok(createTicketDto);
        }
    }
}
