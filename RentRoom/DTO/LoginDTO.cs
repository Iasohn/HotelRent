
using System.ComponentModel.DataAnnotations;

namespace RentRoom.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; } 


    }
}
