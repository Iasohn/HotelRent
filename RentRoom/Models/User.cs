using Microsoft.AspNetCore.Identity;

namespace RentRoom.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
        public Booking Booking { get; set; } 

        public ICollection<Review> Reviews { get; set; }
    }
}
