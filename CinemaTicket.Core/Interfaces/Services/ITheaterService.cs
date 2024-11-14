using App.Core.Shared;

namespace App.Core.Interfaces.Services
{
    public interface ITheaterService : IService<IEntityDto>
    {
        Task<Result<IEntityDto>> CreateScreeningRoomAsync(IEntityDto screeningRoomDto);
    }
}
