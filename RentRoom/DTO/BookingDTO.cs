using RentRoom.Models;
using System.ComponentModel.DataAnnotations;

namespace RentRoom.DTO
{

    public class BookingDTO
    {
        [Range(200, 600)]
        public int RoomNumber { get; set; }
        public required DateTime RentTime { get; set; }
        public required DateTime ExpTime { get; set; }
    }
}
