using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Shared;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface IShowtimeService : IService<IDto>
    {

        public Task<Result<ShowtimeDto>> GetShowtimeByIdWithSeatsAsync(int id);
    }
}
