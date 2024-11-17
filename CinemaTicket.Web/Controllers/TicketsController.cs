using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using CinemaTicket.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ShowtimeService _showtimeService;
        private readonly CartService _cartService;

        public TicketViewModel TicketViewModel { get; set; } = new TicketViewModel();

        public TicketsController(ShowtimeService showtimeService, CartService cartService)
        {
            _showtimeService = showtimeService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int showtimeId, int? seatNumber)
        {
            var ticket = _cartService.GetCart();
            if (ticket == null)
            {
                ticket = new Ticket()
                {
                    TicketDetails = new List<TicketDetail>(),
                    ShowtimeId = showtimeId
                };

            }
            else
            {
                showtimeId = ticket.ShowtimeId;

                ticket.TicketDetails.Add(new TicketDetail { SeatNumber = seatNumber!.Value, ShowtimeId = showtimeId });
            }

            _cartService.SaveCartSession(ticket);

            ViewData["Tickets"] = ticket.TicketDetails.Count;

            var showtime = await _showtimeService.GetShowtimeByIdAsync(showtimeId);

            TicketViewModel.Showtime = showtime;
            TicketViewModel.ShowtimeId = showtimeId;



            return View(TicketViewModel);
        }
    }
}
