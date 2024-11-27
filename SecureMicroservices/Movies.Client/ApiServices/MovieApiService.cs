﻿using Movies.Client.Models;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movieList = new List<Movie>();
            
            movieList.Add(
                new Movie
                {
                    Id = 1,
                    Genre = "Comics",
                    Title = "Title",
                    Rating = "5",
                    ImageUrl = "image/src",
                    ReleaseDate = DateTime.Now,
                    Owner = "swn"
                }
            );

            return await Task.FromResult(movieList);
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
