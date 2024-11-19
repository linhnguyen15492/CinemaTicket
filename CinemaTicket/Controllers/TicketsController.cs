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
        private readonly ISalesService _salesService;

        public TicketsController(ITicketService ticketService, ISalesService salesService)
        {
            _ticketService = ticketService;
            _salesService = salesService;
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

        [HttpGet("sales-by-show")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesByShow()
        {
            var res = await _salesService.GetSalesByShow();
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("sales-by-movie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesByMovie()
        {
            var res = await _salesService.GetSalesByMovie();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
