using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaTicket.Infrastructure.Repositories.Generics
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly CinemaTicketContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(CinemaTicketContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.GetType().GetProperty("Id")?.SetValue(entity, entity.GetType().GetProperty("Id")?.GetValue(entity));

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual IQueryable<T> Query()
        {
            return _dbSet;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> data)
        {
            await _dbSet.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            var res = _dbSet.Where(predicate).ToList();

            return res;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T?> UpdateAsync(int id, T entity)
        {
            T? item = await _dbSet.FindAsync(id);

            _context.Entry(item!).CurrentValues.SetValues(entity);

            var result = await _context.SaveChangesAsync();

            return item;
        }
    }
}
