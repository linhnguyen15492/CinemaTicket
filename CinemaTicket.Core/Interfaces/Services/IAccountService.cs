using App.Core.Shared;

namespace App.Core.Interfaces.Services
{
    public interface IAccountService<T> : IAuthService<T> where T : class
    {
        Task<Result<T?>> GetByIdAsync(string id);
        Task<Result<T>> DeleteAsync(string id);
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> UpdateAsync(T t);
        Task<Result<T>> CreateAsync(T t);
        Task<Result<IEnumerable<T>>> SearchAsync(object queryObject);
    }
}
