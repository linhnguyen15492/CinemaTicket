using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface ITheaterService
    {
        Task<IEnumerable<Theater>?> GetAllAsync();
    }
}