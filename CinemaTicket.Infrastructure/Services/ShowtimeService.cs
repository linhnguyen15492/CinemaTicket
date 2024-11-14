using App.Core.Domain.Dtos;
using App.Core.Entities;
using App.Core.Interfaces.Repositories;
using App.Core.Interfaces.Services;
using App.Core.Mappers;
using App.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Infrastructure.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IRepository<Showtime> _repository;

        public ShowtimeService(IRepository<Showtime> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEntityDto>> CreateAsync(IEntityDto t)
        {
            try
            {
                var showtime = t as CreateShowtimeDto;

                var result = await _repository.AddAsync(showtime!.ToShowtime());

                if (result is null)
                {
                    return Result<IEntityDto>.Failure("Fail to create showtime");
                }
                else
                {
                    return Result<IEntityDto>.Success(t);
                }
            }
            catch (Exception ex)
            {
                return Result<IEntityDto>.Failure(ex.Message);
            }
        }

        public Task<Result<IEntityDto>> UpdateAsync(IEntityDto t)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IEntityDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (result is null)
            {
                return Result<IEnumerable<IEntityDto>>.Failure("No showtime found!");
            }
            else
            {
                return Result<IEnumerable<IEntityDto>>.Success(result.Select(s => s.ToShowtimeDto()));
            }
        }

        public async Task<Result<IEntityDto?>> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);

            if (res is null)
            {
                return Result<IEntityDto?>.Failure("Showtime not found!");

            }

            return Result<IEntityDto?>.Success(res.ToShowtimeDto());
        }

        private async Task<Showtime?> GetShowtimeByIdAsync(int id)
        {
            var res = await _repository.Query().Where(s => s.Id == id)
                .Include(s => s.Movie)
                .Include(s => s.ScreeningRoom)
                    .ThenInclude(r => r.ScreeningRoomType)
                .Include(s => s.ShowtimeSchedule)
                .FirstOrDefaultAsync();

            return res;
        }

        public async Task<Result<IEnumerable<IEntityDto>>> SearchAsync(object queryObject)
        {
            var req = queryObject as ShowtimeQuery;

            IQueryable<Showtime> query = _repository.GetAll()
                                                    .Include(s => s.Movie)
                                                    .Include(s => s.ScreeningRoom)
                                                        .ThenInclude(r => r.ScreeningRoomType)
                                                    .Include(s => s.ShowtimeSchedule);

            if (req!.Movie is not null)
            {
                query = query.Where(s => s.Movie.Title.ToLower().Contains(req.Movie.ToLower()));
            }

            if (req!.ScheduleId is not null)
            {
                query = query.Where(s => (int)s.ShowtimeScheduleId == req.ScheduleId);
            }

            if (req!.Date is not null)
            {
                query = query.Where(s => s.Date.Equals(DateOnly.Parse(req.Date)));
            }

            var res = await query.Select(s => s.ToShowtimeDto()).ToListAsync();

            return Result<IEnumerable<IEntityDto>>.Success(res);
        }

        public Task<Result<IEntityDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class ShowtimeQuery
    {
        public string? Date { get; set; }
        public string? Movie { get; set; }
        public int? ScheduleId { get; set; }
    }
}
