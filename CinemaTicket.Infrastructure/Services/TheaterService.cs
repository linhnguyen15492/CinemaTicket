using App.Core.Domain.Dtos;
using App.Core.Domain.Entities.eShop;
using App.Core.Entities;
using App.Core.Interfaces.Repositories;
using App.Core.Interfaces.Services;
using App.Core.Mappers;
using App.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.Infrastructure.Services
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

        public async Task<Result<IEntityDto>> CreateAsync(IEntityDto t)
        {
            var theaterDto = t as CreateTheaterDto;

            try
            {
                var res = await _theaterRepository.AddAsync(theaterDto!.ToTheater());

                return Result<IEntityDto>.Success(t);

            }
            catch (Exception e)
            {
                return Result<IEntityDto>.Failure(e.Message);
            }

        }

        public async Task<Result<IEntityDto>> CreateScreeningRoomAsync(IEntityDto screeningRoomDto)
        {
            var dto = screeningRoomDto as CreateScreeningRoomDto;

            var theater = await _theaterRepository.GetByIdAsync(dto!.TheaterId);

            if (theater == null)
            {
                return Result<IEntityDto>.Failure("Theater not found");
            }
            else
            {
                var screeningRoom = dto.ToScreeningRoom();
                await _screeningRoomRepository.AddAsync(screeningRoom);

                var result = await _theaterRepository.GetByIdAsync(screeningRoom.TheaterId);

                if (result == null)
                {
                    return Result<IEntityDto>.Failure("Theater not found");
                }
                else
                {
                    return Result<IEntityDto>.Success(result.ToTheaterDto());
                }
            }
        }

        public async Task<Result<IEnumerable<IEntityDto>>> SearchAsync(object queryObject)
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
                    return Result<IEnumerable<IEntityDto>>.Failure("Not found");
                }
                else
                {
                    var data = p.Select(t => new TheaterDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Location = t.Location,
                    });

                    return Result<IEnumerable<IEntityDto>>.Success(data);
                }
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IEntityDto>>.Failure(ex.Message);
            }
        }

        public Task<Result<IEntityDto>> UpdateAsync(IEntityDto t)
        {
            throw new NotImplementedException();
        }

        Task<Result<IEntityDto>> IService<IEntityDto>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IEntityDto>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Theater> source = await _theaterRepository.GetAllAsync();

                List<Theater> data = source.ToList();

                var res = data.Where(t => t.IsDeleted == false).Select(t => t.ToTheaterDto()).ToList();

                return Result<IEnumerable<IEntityDto>>.Success(res);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IEntityDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEntityDto?>> GetByIdAsync(int id)
        {
            try
            {
                var res = await _theaterRepository.GetByIdAsync(id);

                if (res == null)
                {
                    return Result<IEntityDto?>.Failure("Not found");
                }
                else
                {

                    return Result<IEntityDto?>.Success(res.ToTheaterDto());
                };

            }
            catch (Exception ex)
            {
                return Result<IEntityDto?>.Failure(ex.Message);
            }
        }
    }
}
