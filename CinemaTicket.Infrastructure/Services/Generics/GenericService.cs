﻿using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Interfaces.Services;

namespace CinemaTicket.Infrastructure.Services.Generics
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
