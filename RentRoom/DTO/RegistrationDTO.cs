using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentRoom.DTO
{
    public class RegistrationDTO
    {
        [MinLength(5)]
        public required string Username { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [PasswordPropertyText]
        public required string Password { get; set; }

        
    }
}
