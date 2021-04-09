using BAS.AppServices;
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
    }
}
