using BAS.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BAS.Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertReview([FromBody] ReviewDTO reviewDTO)
        {
            var result = await reviewService.InsertReview(reviewDTO);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{userId}/{movieId}")]
        public async Task<IActionResult> DeleteReview([FromRoute] long userId, long movieId)
        {
            var result = await reviewService.DeleteReview(userId, movieId);
            return result ? Ok() : NotFound();
        }
    }
}
