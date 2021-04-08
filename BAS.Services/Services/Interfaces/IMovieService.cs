using BAS.AppServices.DTOs;
using BAS.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.Services.Interfaces
{
    public interface IMovieService
    {
        Task<bool> DeleteMovie(long id);
        Task<Movie> GetMovie(long id);
        Task<MovieListWithFilters> GetMoviesWtihFilter(MovieFilters personnelFilter);
        Task<bool> InsertMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> UpdateMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> InsertReview();
        Task<bool> UpdateReview();
        Task<bool> DeleteReview();
    }
}
