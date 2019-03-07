using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilterDI.Models;
using System.Linq;

namespace FilterDI.Services
{
    public class MovieService : IMovieService
    {
        private List<Movie> _movies = new List<Movie>
        {
               new Movie { Id = 1, Title = "The Matrix", Year = new DateTime(1999,06,11) },
               new Movie { Id = 2, Title = "The Matrix Reloaded", Year = new DateTime(2003,05,21) },
               new Movie { Id = 3, Title = "The Matrix Revolutions", Year = new DateTime(2003,11,27)}
        };

        public async Task<Movie> AddMovie(Movie movie)
        {

            movie.Id = _movies.Last().Id + 1;
            _movies.Add(movie);
            return await Task.FromResult<Movie>(movie);
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await Task.FromResult<Movie>(_movies.FirstOrDefault(movie => movie.Id == id));
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await Task.FromResult<List<Movie>>(_movies);
        }
    }
}