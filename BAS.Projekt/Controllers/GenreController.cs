using BAS.AppServices.DTOs;
using BAS.AppServices.Services;
using BAS.AppServices.Services.Interfaces;
using BAS.Database.Models;
using BAS.Repository.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAS.Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre([FromRoute] long id)
        {
            var genre = await genreService.GetGenre(id);
            return Ok(genre);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres([FromBody] GetGenresFiltersDTO genreFilter)
        {
            var genres = await genreService.GetGenresByName(genreFilter);
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> InsertGenre([FromBody] Genre genre)
        {
            var result = await genreService.InsertGenre(genre);
            return result ? Ok() : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreDTO genre)
        {
            
            var result = await genreService.UpdateGenre(genre);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] long id)
        {
            var result = await genreService.DeleteGenre(id);
            return result ? Ok() : NotFound();
        }
    }
}
