using BAS.AppServices;
using BAS.Projekt.Attributes;
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
        [BASAuthorize]
        public async Task<IActionResult> InsertReview([FromBody] InsertUpdateReviewDTO reviewDTO)
        {
            var result = await reviewService.InsertReview(reviewDTO);
            return result ? Ok() : NotFound();
        }

        [HttpPut]
        [BASAuthorize]
        public async Task<IActionResult> UpdateReview([FromBody] InsertUpdateReviewDTO reviewDTO)
        {
            var result = await reviewService.UpdateReview(reviewDTO);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{userId}/{movieId}")]
        [BASAuthorize]
        public async Task<IActionResult> DeleteReview([FromRoute] long userId, long movieId)
        {
            var result = await reviewService.DeleteReview(userId, movieId);
            return result ? Ok() : NotFound();
        }
    }
}
