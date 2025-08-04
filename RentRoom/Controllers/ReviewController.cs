using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RentRoom.DTO;
using RentRoom.Interfaces;
using RentRoom.Models;

namespace RentRoom.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewInterface _review;
        public ReviewController(ReviewInterface review)
        {
            _review = review;
        }

        [HttpPost]
        public async Task<IActionResult> PostRoom(Review review)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Something wrong");
            }

            await _review.PostReview(review);


            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomScale(ReviewFilterDTO filter)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("fuck");
            }    

           var reviews = _review.GetReviewWithScale(filter);

            if (reviews == null  )
            {
                return NotFound("No reviews found.");
            }
            return BadRequest("smth wrong");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
           var reviews = await _review.GetAllReviews();
           return Ok(reviews);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteReview(int id)
        {
            
            
            await _review.DeleteReview(id); 
            
            return Ok();
        }

    }
}
