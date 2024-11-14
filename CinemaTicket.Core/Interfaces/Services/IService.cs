using App.Core.Shared;

namespace App.Core.Interfaces.Services
{
    public interface IService<T> where T : class
    {
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T?>> GetByIdAsync(int id);
        Task<Result<T>> UpdateAsync(T t);
        Task<Result<T>> CreateAsync(T t);
        Task<Result<T>> DeleteAsync(int id);
        Task<Result<IEnumerable<T>>> SearchAsync(object queryObject);
    }
}

