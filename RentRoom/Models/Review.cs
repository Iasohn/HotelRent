using System.ComponentModel.DataAnnotations;

namespace RentRoom.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User user { get; set; }

        public required string TextReview { get; set; }

        [Range (1,5)]
        public required int Rating { get; set; }

        public Room room { get; set; }
        public int roomId { get; set; }
    }
}
