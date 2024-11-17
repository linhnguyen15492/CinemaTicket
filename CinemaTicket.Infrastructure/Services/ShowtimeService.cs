using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Shared;
using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using CinemaTicket.Infrastructure.Repositories;

namespace CinemaTicket.Infrastructure.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IRepository<Showtime> _showtimeRepository;
        private readonly IRepository<Seat> _seatRepository;

        public ShowtimeService(IRepository<Showtime> showtimeRepository, IRepository<Seat> seatRepository)
        {
            _showtimeRepository = showtimeRepository;
            _seatRepository = seatRepository;
        }

        public async Task<Result<IDto>> CreateAsync(IDto t)
        {
            try
            {
                var showtime = t as CreateShowtimeDto;

                var result = await _showtimeRepository.AddAsync(showtime!.ToShowtime());

                if (result is not null)
                {
                    var seats = await GenerateSeats(result.Id);

                    if (seats is not null)
                    {
                        await _seatRepository.AddRangeAsync(seats);

                        return Result<IDto>.Success(t);
                    }

                }

                return Result<IDto>.Failure("Fail to create showtime");
            }
            catch (Exception ex)
            {
                return Result<IDto>.Failure(ex.Message);
            }
        }

        public Task<Result<IDto>> UpdateAsync(IDto t)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IDto>>> GetAllAsync()
        {
            var result = await _showtimeRepository.GetAllAsync();

            if (result is null)
            {
                return Result<IEnumerable<IDto>>.Failure("No showtime found!");
            }
            else
            {
                return Result<IEnumerable<IDto>>.Success(result.Select(s => s.ToShowtimeDto()));
            }
        }

        public async Task<Result<IDto?>> GetByIdAsync(int id)
        {
            var res = await _showtimeRepository.GetByIdAsync(id);

            if (res is null)
            {
                return Result<IDto?>.Failure("Showtime not found!");

            }

            return Result<IDto?>.Success(res.ToShowtimeDto());
        }

        private async Task<Showtime?> GetShowtimeByIdAsync(int id)
        {
            var res = await _showtimeRepository.Query().Where(s => s.Id == id).FirstOrDefaultAsync();

            return res;
        }

        public async Task<Result<IEnumerable<IDto>>> SearchAsync(object queryObject)
        {
            var req = queryObject as ShowtimeQuery;

            IQueryable<Showtime> query = _showtimeRepository.GetAll()
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

            return Result<IEnumerable<IDto>>.Success(res);
        }

        public Task<Result<IDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        private async Task<List<Seat>?> GenerateSeats(int showtimeId)
        {
            var showtime = await GetShowtimeByIdAsync(showtimeId);

            var seats = new List<Seat>();
            if (showtime is not null)
            {
                for (int i = 1; i <= showtime.ScreeningRoom?.Capacity; i++)
                {
                    seats.Add(new Seat { SeatNumber = i, ShowtimeId = showtimeId });
                }
            }

            return seats;
        }

        public async Task<Result<ShowtimeDto>> GetShowtimeByIdWithSeatsAsync(int id)
        {

            var showtime = await _showtimeRepository.Query()
                                               .Where(s => s.Id == id)
                                                    .Include(s => s.Movie)
                                                    .Include(s => s.ScreeningRoom)
                                                        .ThenInclude(r => r.ScreeningRoomType)
                                                    .Include(s => s.ShowtimeSchedule)
                                                    .Include(s => s.Seats)
                                               .FirstOrDefaultAsync();



            if (showtime is not null)
            {
                return Result<ShowtimeDto>.Success(showtime.ToShowtimeDto());
            }

            return Result<ShowtimeDto>.Failure("Showtime not found!");
        }
    }

    public class ShowtimeQuery
    {
        public string? Date { get; set; }
        public string? Movie { get; set; }
        public int? ScheduleId { get; set; }
    }
}
