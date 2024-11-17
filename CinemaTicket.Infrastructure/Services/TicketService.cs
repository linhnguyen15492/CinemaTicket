using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Shared;
using Microsoft.EntityFrameworkCore;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Mappers;
using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _repository;

        public TicketService(IRepository<Ticket> repository)
        {
            _repository = repository;
        }

        public Task<Result<IDto>> CreateAsync(IDto t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> UpdateAsync(IDto t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IDto>>> GetAllAsync()
        {
            //var res = await _repository.GetAll().Include(t => t.Showtime)
            //                        .ThenInclude(s => s.Movie)
            //                   .Include(t => t.Showtime)
            //                        .ThenInclude(s => s.ScreeningRoom)
            //                            .ThenInclude(s => s.Theater)
            //                    .Include(t => t.Status)
            //                    .ToListAsync();

            var res = await _repository.GetAll().Include(t => t.TicketDetails)
                    .ToListAsync();


            if (res is null)
            {
                return Result<IEnumerable<IDto>>.Failure("No ticket found!");
            }
            else
            {
                return Result<IEnumerable<IDto>>.Success(res.Select(t => t.ToTicketDto()));
            }
        }

        public Task<Result<IDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<IDto>>> SearchAsync(object queryObject)
        {
            throw new NotImplementedException();
        }
    }
}
