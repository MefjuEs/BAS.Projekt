using BAS.Database;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface IMovieService
    {
        Task<bool> InsertMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> UpdateMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> DeleteMovie(long id);
        Task<Movie> GetMovie(long id);
        Task<MovieListWithFilters> GetMovieWithFilters(MovieFilters personnelFilter);
        Task<bool> DoesMovieExist(long movieId);
        Task<bool> UpdateMovieRating(long movieId, double avgRating);
    }
}
