using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Shared;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface ITheaterService : IService<IDto>
    {
        Task<Result<IDto>> CreateScreeningRoomAsync(IDto screeningRoomDto);
    }
}
