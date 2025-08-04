using Microsoft.EntityFrameworkCore;
using RentRoom.DbContextProj;
using RentRoom.DTO;
using RentRoom.Interfaces;
using RentRoom.Models;

namespace RentRoom.Repository
{
    public class ReviewRepository : ReviewInterface
    {
        private readonly ProjectDbContext _context;
        public ReviewRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task DeleteReview(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Review>> GetAllReviews()
        {
            var Reviews = await _context.Reviews.AsNoTracking().ToListAsync();
            return Reviews;
        }

        public async Task<List<Review>> GetReviewWithScale(ReviewFilterDTO review)
        {
            var Reviews = _context.Reviews.AsQueryable();

            if (review.ReviewWithBadScale == true)
            {
                Reviews =  Reviews.Where(x => x.Rating <= 3);
            }    

            if(review.ReviewWithoutBadScale == true)
            {
                Reviews = Reviews.Where(x => x.Rating > 3);
            }

            return await Reviews.ToListAsync();
        }

        public async Task PostReview(Review review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();

        }
    }
}
