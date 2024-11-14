using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Shared;
using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Infrastructure.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly IRepository<Theater> _theaterRepository;
        private readonly IRepository<ScreeningRoom> _screeningRoomRepository;

        public TheaterService(IRepository<Theater> theaterRepository, IRepository<ScreeningRoom> screeningRoomRepository)
        {
            _theaterRepository = theaterRepository;
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<Result<IDto>> CreateAsync(IDto t)
        {
            var theaterDto = t as CreateTheaterDto;

            try
            {
                var res = await _theaterRepository.AddAsync(theaterDto!.ToTheater());

                return Result<IDto>.Success(t);

            }
            catch (Exception e)
            {
                return Result<IDto>.Failure(e.Message);
            }

        }

        public async Task<Result<IDto>> CreateScreeningRoomAsync(IDto screeningRoomDto)
        {
            var dto = screeningRoomDto as CreateScreeningRoomDto;

            var theater = await _theaterRepository.GetByIdAsync(dto!.TheaterId);

            if (theater == null)
            {
                return Result<IDto>.Failure("Theater not found");
            }
            else
            {
                var screeningRoom = dto.ToScreeningRoom();
                await _screeningRoomRepository.AddAsync(screeningRoom);

                var result = await _theaterRepository.GetByIdAsync(screeningRoom.TheaterId);

                if (result == null)
                {
                    return Result<IDto>.Failure("Theater not found");
                }
                else
                {
                    return Result<IDto>.Success(result.ToTheaterDto());
                }
            }
        }

        public async Task<Result<IEnumerable<IDto>>> SearchAsync(object queryObject)
        {
            var query = queryObject as string;

            if (string.IsNullOrEmpty(query))
            {
                return await GetAllAsync();
            }

            try
            {
                var p = await _theaterRepository.Query().Where(t => t.Name.ToLower().Contains(query.ToLower())).ToListAsync();

                if (p.Count == 0)
                {
                    return Result<IEnumerable<IDto>>.Failure("Not found");
                }
                else
                {
                    var data = p.Select(t => new TheaterDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Location = t.Location,
                    });

                    return Result<IEnumerable<IDto>>.Success(data);
                }
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IDto>>.Failure(ex.Message);
            }
        }

        public Task<Result<IDto>> UpdateAsync(IDto t)
        {
            throw new NotImplementedException();
        }

        Task<Result<IDto>> IService<IDto>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IDto>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Theater> source = await _theaterRepository.GetAllAsync();

                List<Theater> data = source.ToList();

                var res = data.Where(t => t.IsDeleted == false).Select(t => t.ToTheaterDto()).ToList();

                return Result<IEnumerable<IDto>>.Success(res);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IDto?>> GetByIdAsync(int id)
        {
            try
            {
                var res = await _theaterRepository.GetByIdAsync(id);

                if (res == null)
                {
                    return Result<IDto?>.Failure("Not found");
                }
                else
                {

                    return Result<IDto?>.Success(res.ToTheaterDto());
                };

            }
            catch (Exception ex)
            {
                return Result<IDto?>.Failure(ex.Message);
            }
        }
    }
}
