using App.Core.Entities;

namespace App.Core.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
    }
}
