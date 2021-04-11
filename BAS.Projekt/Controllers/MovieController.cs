﻿using BAS.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BAS.Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertMovie([FromForm] InsertUpdateMovieDTO movieDTO)
        {
            var result = await movieService.InsertMovie(movieDTO);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromForm] InsertUpdateMovieDTO movieDTO)
        {
            var result = await movieService.UpdateMovie(movieDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] long id)
        {
            var result = await movieService.DeleteMovie(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] long id)
        {
            var result = await movieService.GetMovie(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesWithFilters([FromBody] MovieFilters filters)
        {
            var result = await movieService.GetMovieWithFilters(filters);
            return Ok(result);
        }

        [HttpGet("Reviews")]
        public async Task<IActionResult> GetMovieReviews([FromBody] ReviewFilters filters)
        {
            var result = await movieService.GetMovieReviews(filters);
            return Ok(result);
        }

        #region AlternativeMethods
        [HttpGet("{id}/Genres")]
        public async Task<IActionResult> GetMovieGenres([FromRoute] long id)
        {
            var result = await movieService.GetMovieGenres(id);
            return Ok(result);
        }

        [HttpGet("{id}/Personnel")]
        public async Task<IActionResult> GetMoviePersonnel([FromRoute] long id)
        {
            var result = await movieService.GetMoviePersonnel(id);
            return Ok(result);
        }

        [HttpGet("{id}/Poster")]
        public async Task<IActionResult> GetMoviePoster([FromRoute] long id)
        {
            var result = await movieService.GetMoviePoster(id);
            return Ok(result);
        }
        #endregion
    }
}
