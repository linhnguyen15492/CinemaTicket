using App.Core.Shared;

namespace App.Core.Interfaces.Services
{
    public interface IPaging<T> where T : class
    {
        Task<ApiResponse<PaginatedList<T>>> GetAllAsync(int pageIndex);
    }
}
