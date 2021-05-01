using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface IMovieService
    {
        Task<bool> InsertMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> UpdateMovie(InsertUpdateMovieDTO movieDTO);
        Task<bool> DeleteMovie(long id);
        Task<GetMovieDTO> GetMovie(long id);
        //Task<MovieReviewListWithFilters> GetMovieReviews(ReviewFiltersDTO reviewfilters);
        Task<List<GenreInMovieDTO>> GetMovieGenres(long movieId);
        Task<List<PersonnelInMovieDTO>> GetMoviePersonnel(long movieId);
        Task<MovieListWithFilters> GetMovieWithFilters(MovieFilters personnelFilter);
        Task<bool> DoesMovieExist(long movieId);
        Task<bool> UpdateMovieRating(long movieId, double avgRating);
        Task<FileDTO> GetMoviePoster(long movieId);
        Task<RecommendationsDTO> GetRecommendations(long userAccountId, int page = 1, int pageSize = 20);
    }
}
