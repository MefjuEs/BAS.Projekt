using BAS.AppCommon;
using BAS.Database;
using System.Linq;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public class ReviewService : IReviewService
    {
        private readonly MovieDbContext db;
        private readonly IAuthService authServices;
        private readonly IMovieService movieServices;

        public ReviewService(MovieDbContext db, IAuthService authServices, IMovieService movieServices)
        {
            this.db = db;
            this.authServices = authServices;
            this.movieServices = movieServices;
        }

        public async Task<bool> InsertReview(ReviewDTO reviewDTO)
        {
            if (!await authServices.DoesUserExist(reviewDTO.UserId))
                return false;

            if (!await movieServices.DoesMovieExist(reviewDTO.MovieId))
                return false;

            if (reviewDTO.Rating < 1 || reviewDTO.Rating > 10)
                return false;

            if (reviewDTO.Message.Length > StaticValues.ReviewContentMaxLength)
                return false;

            var review = new Review()
            {
                UserId = reviewDTO.UserId,
                MovieId = reviewDTO.MovieId,
                Message = reviewDTO.Message,
                Rating = reviewDTO.Rating
            };

            db.Reviews.Add(review);
            db.SaveChanges();

            var avgRating = GetAvgMovieRating(reviewDTO.MovieId);
            await movieServices.UpdateMovieRating(reviewDTO.MovieId, avgRating);

            return true;
        }

        public double GetAvgMovieRating(long movieId)
        {
            var reviews = db.Reviews.Where(m => m.MovieId == movieId).ToList();

            if(reviews == null || reviews.Count == 0)
            {
                return 0.0;
            }

            double sumRating = 0.0;
            foreach(var review in reviews)
            {
                sumRating += review.Rating;
            }

            return sumRating / reviews.Count();
        }

        public async Task<bool> DeleteReview(long userId, long movieId)
        {
            var review = await db.Reviews.FindAsync(userId, movieId);

            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();

                var avgRating = GetAvgMovieRating(movieId);
                await movieServices.UpdateMovieRating(movieId, avgRating);

                return true;
            }

            return false;
        }
    }
}
