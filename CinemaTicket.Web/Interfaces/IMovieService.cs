using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>?> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
    }
}
