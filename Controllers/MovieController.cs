using System;
using System.Threading.Tasks;
using FilterDI.Services;
using Microsoft.AspNetCore.Mvc;
using FilterDI.Filters;
using FilterDI.Models;
using Microsoft.AspNetCore.Authorization;

namespace FilterDI.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("api/movie/{id}")]
        [TypeFilter(typeof(SecurityFilterUsingFactory))]
        public async Task<ActionResult> GetMovieByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/movie")]
        public async Task<ActionResult> AddMovieAsync(Movie movie)
        {
            if (movie == null)
                return BadRequest(movie);

            var addMovieResult = await _movieService.AddMovie(movie);

            return Ok(addMovieResult);
        }

        [ServiceFilter(typeof(SecurityFilter))]
        [HttpGet("api/movies/all")]
        public async Task<ActionResult> GetMoviesAsync()
        {
            return Ok(await _movieService.GetMoviesAsync());
        }
    }
}