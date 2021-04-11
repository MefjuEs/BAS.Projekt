using BAS.AppCommon;
using BAS.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> InsertReview(InsertUpdateReviewDTO reviewDTO)
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

        private double GetAvgMovieRating(long movieId)
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

        public async Task<bool> UpdateReview(InsertUpdateReviewDTO reviewDTO)
        {
            if (!await authServices.DoesUserExist(reviewDTO.UserId))
                return false;

            if (!await movieServices.DoesMovieExist(reviewDTO.MovieId))
                return false;

            if (reviewDTO.Rating < 1 || reviewDTO.Rating > 10)
                return false;

            if (reviewDTO.Message.Length > StaticValues.ReviewContentMaxLength)
                return false;


            var review = db.Reviews.Find(reviewDTO.UserId, reviewDTO.MovieId);
            review.Message = reviewDTO.Message;
            review.Rating = reviewDTO.Rating;

            db.Reviews.Update(review);
            db.SaveChanges();

            var avgRating = GetAvgMovieRating(reviewDTO.MovieId);
            await movieServices.UpdateMovieRating(reviewDTO.MovieId, avgRating);

            return true;
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

        public async Task<Review> GetReview(long userId, long movieId)
        {
            return await db.Reviews.FindAsync(userId, movieId);
        }

        public UserReviewListWithFilters GetUserReviews(ReviewFilters reviewfilters)
        {
            var pageSize = reviewfilters.PageSize ?? int.MaxValue;

            var result = new UserReviewListWithFilters()
            {
                CurrentPage = reviewfilters.Page,
                PageSize = pageSize,
                AllPages = (int)Math.Ceiling(db.Reviews.Count(r => r.UserId == reviewfilters.Id) * 1.0 / pageSize)
            };

            var reviews = db.Reviews.Include(r => r.Movie)
                .Where(r => r.UserId == reviewfilters.Id)
                .Select(r => new UserReviewInListDTO()
                {
                    UserId = r.UserId,
                    MovieId = r.MovieId,
                    Rating = r.Rating,
                    Message = r.Message,
                    MovieTitle = r.Movie.Title
                });

            switch (reviewfilters.OrderBy.ToLower())
            {
                case "rating":
                    if (reviewfilters.IsDescending)
                        reviews = reviews.OrderByDescending(r => r.Rating);
                    else
                        reviews = reviews.OrderBy(r => r.Rating);
                    break;
                case "movie":
                    if (reviewfilters.IsDescending)
                        reviews = reviews.OrderByDescending(r => r.MovieTitle);
                    else
                        reviews = reviews.OrderBy(r => r.MovieTitle);
                    break;
                default:
                    break;
            }

            reviews = reviews.Skip((reviewfilters.Page - 1) * pageSize).Take(pageSize);

            result.ReviewList = reviews.ToList();

            return result;
        }
    }
}
