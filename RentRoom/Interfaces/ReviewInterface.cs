using RentRoom.DTO;
using RentRoom.Models;

namespace RentRoom.Interfaces
{
    public interface ReviewInterface
    {
        public Task PostReview(Review review);
        public Task DeleteReview(int id);
        public Task<List<Review>> GetReviewWithScale(ReviewFilterDTO review);
        public Task<List<Review>> GetAllReviews();
    }
}
