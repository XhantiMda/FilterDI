using System.Collections.Generic;
using System.Threading.Tasks;
using FilterDI.Models;

namespace FilterDI.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieById(int id);
        Task<Movie> AddMovie(Movie movie);
    }
}
