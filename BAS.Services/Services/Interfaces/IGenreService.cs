using BAS.AppServices.DTOs;
using BAS.Database.Models;
using System.Threading.Tasks;

namespace BAS.AppServices.Services.Interfaces
{
    public interface IGenreService
    {
        Task<bool> DeleteGenre(long id);
        Task<Genre> GetGenre(long id);
        Task<GenreListWithFilters> GetGenresByName(GetGenresFiltersDTO genreFilter);
        Task<bool> InsertGenre(Genre genre);
        Task<bool> UpdateGenre(GenreDTO genre);
        Task<bool> IsGenreInDB(long id);
    }
}