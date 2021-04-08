using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface IReviewService
    {
        Task<bool> InsertReview(ReviewDTO review);
        Task<bool> DeleteReview(long userId, long movieId);
    }
}
