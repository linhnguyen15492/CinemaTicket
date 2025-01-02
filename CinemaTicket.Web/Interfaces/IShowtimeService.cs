using CinemaTicket.Web.Dtos;
using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface IShowtimeService
    {
        Task<Showtime?> AddAsync(CreateShowtimeDto createShowtimeDto);
        Task<IEnumerable<Showtime>?> GetAllAsync();
        Task<Showtime?> GetShowtimeByIdAsync(int showtimeId);
    }
}