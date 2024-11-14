using CinemaTicket.Core.Shared;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface IPaging<T> where T : class
    {
        Task<ApiResponse<PaginatedList<T>>> GetAllAsync(int pageIndex);
    }
}
