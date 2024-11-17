using CinemaTicket.Web.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTicket.Web.ViewModels
{
    public class CreateShowtimeViewModel : BaseViewModel
    {
        public CreateShowtimeDto CreateShowtimeDto { get; set; } = default!;

        public SelectList ScreeningRooms { set; get; } = default!;

        public string TheaterName { get; set; } = string.Empty;

        public SelectList Movies { set; get; } = default!;

        public SelectList ShowtimeSchedules { set; get; } = default!;
    }
}
