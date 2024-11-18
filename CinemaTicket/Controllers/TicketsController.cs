using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _ticketService.GetAllAsync();
            if (res.IsSuccess)
            {

                return Ok(res.Value!);
            }
            else
            {
                return BadRequest(res.Errors);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = UserRoles.Administrator + "," + UserRoles.TicketSeller)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTicketDto ticket)
        {
            var res = await _ticketService.CreateAsync(ticket);

            if (res.IsSuccess)
            {
                return Ok(res.Value!);
            }
            else
            {
                return BadRequest(res.Errors);
            }
        }
    }
}
