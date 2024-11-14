using CinemaTicket.Core.Entities;

namespace CinemaTicket.Core.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
    }
}
