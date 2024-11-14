using App.Core.Interfaces.Repositories;
using App.Core.Interfaces.Services;
using App.Core.Shared;
using App.Core.Mappers;
using Microsoft.EntityFrameworkCore;
using App.Core.Entities;
using App.Infrastructure.Dtos;


namespace App.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;

        public MovieService(IRepository<Movie> repository)
        {
            _repository = repository;
        }


        public async Task<Result<IEntityDto>> CreateAsync(IEntityDto t)
        {
            var movieDto = t as CreateMovieDto;
            var res = await _repository.AddAsync(movieDto!.ToMovie());

            if (res is not null)
            {
                return Result<IEntityDto>.Success(t);
            }
            else
            {
                return Result<IEntityDto>.Failure("Fail to create movie");
            }
        }

        public Task<Result<IEntityDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IEntityDto>>> GetAllAsync()
        {
            try
            {
                var movies = await _repository.GetAllAsync();

                var data = movies.Select(m => m.ToMovieDto());

                return Result<IEnumerable<IEntityDto>>.Success(data);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IEntityDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEntityDto?>> GetByIdAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);

            if (movie is not null)
            {
                return Result<IEntityDto?>.Success(movie.ToMovieDto());
            }
            else
            {
                return Result<IEntityDto?>.Failure("Movie not found");
            }
        }

        public Task<Result<IEnumerable<IEntityDto>>> SearchAsync(object queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEntityDto>> UpdateAsync(IEntityDto t)
        {
            throw new NotImplementedException();
        }

        private async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var m = await _repository.Query().Where(m => m.Id == id).Include(m => m.Status).FirstOrDefaultAsync();
            return m;
        }

    }
}
