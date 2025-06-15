using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using RentRoom.DbContextProj;
using RentRoom.DTO;
using RentRoom.Models;

namespace RentRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly UserManager<User> _userManager;



        public BookingController(ProjectDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("PostBooking")]
        [Authorize]
        public async Task<IActionResult> BookingService([FromBody] BookingDTO booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(m => m.RoomNumber == booking.RoomNumber);
            var UserID = HttpContext.User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(UserID))
                return Unauthorized("User ID not found in token.");

            if (room == null)
            {
                return BadRequest("This room not founded");
            }

            if (_context.Bookings.Any(b => b.RentRoom.Id == room.Id))
            {
                return BadRequest("This room is already rented. Please choose another one :(");
            }


            var User = await _userManager.FindByIdAsync(UserID);

            if (User == null)
            {
                return BadRequest("User not found!");
            }


            if (booking.ExpTime <= booking.RentTime)
            {
                return BadRequest("Expiration time must be greater than rent time.");
            }


            var Book = new Booking
            {
                RentRoom = room,
                RoomNumber = booking.RoomNumber,
                RentUser = User,
                ExpTime = booking.ExpTime,
                RentTime = booking.RentTime,
            };

            await _context.Bookings.AddAsync(Book);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "We will wait for you :)",
                BookingId = Book.Id,
                RoomNumber = room.RoomNumber,
                ExpiryDate = booking.ExpTime
            });

        }

        [HttpGet("GetExpiredBookings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExpiredBookings()
        {


            var Bookings = await _context.Bookings.Where(r => r.ExpTime <= DateTime.UtcNow).ToListAsync();
            return Ok(Bookings);
        }

        [HttpDelete("DeleteExpiredBookings{id:int}")]
        public async Task<IActionResult> DeleteExpiredBookings([FromRoute] int id)
        {            
            var Booking = await _context.Bookings.FindAsync(id);
            if(Booking == null)
            {
                return BadRequest("Booking not founded!");
            }

            _context.Remove(Booking);
            await _context.SaveChangesAsync();
            return Ok("Succesfully deleted");
        }

        [HttpPost("ApproveBooking/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveBooking([FromRoute] int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.Id == id);

            if(booking == null)
            {
                return BadRequest("Error in search!");
            }

            booking.Confirmation = (IsConfirmed)1;

            await _context.SaveChangesAsync();
            return Ok("Room rent approved!");
        }



    }
}
