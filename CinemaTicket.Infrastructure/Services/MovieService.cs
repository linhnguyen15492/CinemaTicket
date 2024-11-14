using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Core.Shared;
using Microsoft.EntityFrameworkCore;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Mappers;


namespace CinemaTicket.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;

        public MovieService(IRepository<Movie> repository)
        {
            _repository = repository;
        }


        public async Task<Result<IDto>> CreateAsync(IDto t)
        {
            var movieDto = t as CreateMovieDto;
            var res = await _repository.AddAsync(movieDto!.ToMovie());

            if (res is not null)
            {
                return Result<IDto>.Success(t);
            }
            else
            {
                return Result<IDto>.Failure("Fail to create movie");
            }
        }

        public Task<Result<IDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<IDto>>> GetAllAsync()
        {
            try
            {
                var movies = await _repository.GetAllAsync();

                var data = movies.Select(m => m.ToMovieDto());

                return Result<IEnumerable<IDto>>.Success(data);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IDto?>> GetByIdAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);

            if (movie is not null)
            {
                return Result<IDto?>.Success(movie.ToMovieDto());
            }
            else
            {
                return Result<IDto?>.Failure("Movie not found");
            }
        }

        public Task<Result<IEnumerable<IDto>>> SearchAsync(object queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> UpdateAsync(IDto t)
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
