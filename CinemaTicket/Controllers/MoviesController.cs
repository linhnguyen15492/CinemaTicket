﻿using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _service.GetAllAsync();
            if (res.IsSuccess)
            {
                return Ok((IEnumerable<MovieDto>)res.Value!);
            }
            else
            {
                return BadRequest(res.Errors!);
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
                return Ok(res.Value!);
            }
            else
            {
                return NotFound(res.Errors!);
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
                return Ok(result.Value!);
            }
            else
            {
                return BadRequest(result.Errors!);
            }
        }
    }
}
