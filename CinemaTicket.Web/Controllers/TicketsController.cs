using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using CinemaTicket.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Sockets;

namespace CinemaTicket.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ShowtimeService _showtimeService;
        private readonly TicketService _ticketService;
        private readonly CartService _cartService;
        private readonly UserService _userService;

        public TicketViewModel TicketViewModel { get; set; } = new TicketViewModel();

        public TicketsController(ShowtimeService showtimeService, CartService cartService, TicketService ticketService, UserService userService)
        {
            _showtimeService = showtimeService;
            _cartService = cartService;
            _ticketService = ticketService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int showtimeId)
        {
            if (!_userService.IsLoggedIn)
            {
                return RedirectToAction("Index", "Account");
            }

            var roles = _userService.GetRoles();

            if (!roles.Contains("Administrator") && !roles.Contains("TicketSeller"))
            {
                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Bạn không có quyền thực hiện yêu cầu này!"
                };

                return View("Error", vm);

                //return Unauthorized("Bạn không có quyền thực hiện!");
            }


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
                _cartService.ClearCart();
                ticket.ShowtimeId = showtimeId;
            }

            _cartService.SaveCartSession(ticket);

            ViewData["Tickets"] = ticket.TicketDetails.Count;
            ViewData["Amount"] = ticket.TicketDetails.Sum(t => t.Price);

            var showtime = await _showtimeService.GetShowtimeByIdAsync(showtimeId);

            TicketViewModel.Showtime = showtime!;
            TicketViewModel.ShowtimeId = showtimeId;

            return View(TicketViewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(int showtimeId, int seatNumber, double price)
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

            var ticketDetail = ticket.TicketDetails.FirstOrDefault(td => td.SeatNumber == seatNumber && td.ShowtimeId == showtimeId);

            if (ticketDetail is null)
            {
                // Chưa tồn tại, add vào cart
                ticket.TicketDetails.Add(new TicketDetail { SeatNumber = seatNumber, ShowtimeId = showtimeId, Price = price });
            }

            _cartService.SaveCartSession(ticket);

            return RedirectToAction(nameof(Index), new { showtimeId = ticket!.ShowtimeId });
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int seatNumber)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var ticket = _cartService.GetCart();

            var detail = ticket.TicketDetails.FirstOrDefault(p => p.SeatNumber == seatNumber);

            if (detail != null)
            {
                // Đã tồn tại, tăng thêm 1
                ticket.TicketDetails.Add(new TicketDetail { SeatNumber = seatNumber, ShowtimeId = ticket.ShowtimeId });
            }

            _cartService.SaveCartSession(ticket);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/removecart/{seatNumber:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int seatNumber)
        {
            var ticket = _cartService.GetCart();

            var cartitem = ticket.TicketDetails.FirstOrDefault(p => p.SeatNumber == seatNumber);

            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                ticket.TicketDetails.Remove(cartitem);
            }

            _cartService.SaveCartSession(ticket);
            return RedirectToAction(nameof(Ticket));
        }

        public IActionResult Ticket(bool? success, int? ticketId)
        {
            if (success == true)
            {
                ViewData["Message"] = $"Đặt vé thành công, ID: {ticketId}";
            }
            else
            {
                ViewData["Message"] = "Đặt vé thất bại";
            }

            var cart = _cartService.GetCart();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var ticket = _cartService.GetCart();

            if (ticket!.TicketDetails.Count == 0)
            {
                return RedirectToAction(nameof(Index), new { showtimeId = ticket.ShowtimeId });
            }

            var res = await _ticketService.AddAsync(ticket);

            if (res is not null)
            {
                _cartService.ClearCart();

                ViewData["Message"] = $"Đặt vé thành công, ID: {res.TicketId}, suất chiếu: {res.ShowtimeId}";

                return RedirectToAction(nameof(Ticket), new { success = true, ticketId = res.TicketId });
            }
            else
            {
                ViewData["Message"] = "Đặt vé thất bại";
            }

            return RedirectToAction(nameof(Ticket), new { success = false });
        }
    }
}
