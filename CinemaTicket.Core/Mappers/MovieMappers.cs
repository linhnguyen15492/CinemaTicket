using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums;

namespace CinemaTicket.Core.Mappers
{
    public static class MovieMappers
    {
        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Status = movie.Status.Description,
            };
        }

        public static Movie ToMovie(this MovieDto movieDto)
        {
            return new Movie
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                Description = movieDto.Description,
                Genre = movieDto.Genre,
                StatusId = (MovieStatusEnum)movieDto.StatusId,
                CreatedDate = DateTime.UtcNow,
            };
        }

        public static Movie ToMovie(this CreateMovieDto movieDto)
        {
            return new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                Genre = movieDto.Genre,
                CreatedDate = DateTime.UtcNow,
            };
        }
    }
}