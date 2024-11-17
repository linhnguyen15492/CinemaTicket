﻿using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Services
{
    public class MovieService : BaseService, IMovieService
    {

        public MovieService(string baseUrl)
        {
            BaseUrl=baseUrl;
        }

        public async Task<IEnumerable<Movie>?> GetAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage response = await client.GetAsync("movies");
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadFromJsonAsync<IEnumerable<Movie>>();
                return movies;
            }
            else
            {
                return null;
            }
        }
    }
}