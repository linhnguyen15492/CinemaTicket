using System.Linq.Expressions;

namespace CinemaTicket.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> data);
        Task<T> UpdateAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);

        /// <summary>
        /// hard delete entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// soft delete entity by setting IsDeleted to true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        IQueryable<T> Query();

        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
    }

}
