using App.Core.Domain.Dtos;
using App.Core.Interfaces.Repositories;
using App.Core.Interfaces.Services;
using App.Core.Shared;
using Microsoft.EntityFrameworkCore;
using App.Core.Mappers;
using App.Core.Entities;

namespace App.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<TicketBooking> _repository;

        public TicketService(IRepository<TicketBooking> repository)
        {
            _repository = repository;
        }

        public Task<Result<IEntityDto>> CreateAsync(IEntityDto t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEntityDto>> UpdateAsync(IEntityDto t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEntityDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IEntityDto>>> GetAllAsync()
        {
            var res = await _repository.GetAll().Include(t => t.Showtime)
                                    .ThenInclude(s => s.Movie)
                               .Include(t => t.Showtime)
                                    .ThenInclude(s => s.ScreeningRoom)
                                        .ThenInclude(s => s.Theater)
                                .Include(t => t.Status)
                                .Include(t => t.TicketBookingDetails)
                                .ToListAsync();

            if (res is null)
            {
                return Result<IEnumerable<IEntityDto>>.Failure("No ticket found!");
            }
            else
            {
                return Result<IEnumerable<IEntityDto>>.Success(res.Select(t => t.ToTicketBookingDto()));
            }
        }

        public Task<Result<IEntityDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<IEntityDto>>> SearchAsync(object queryObject)
        {
            throw new NotImplementedException();
        }
    }
}
