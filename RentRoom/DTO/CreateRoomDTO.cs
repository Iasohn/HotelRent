using RentRoom.Models;

namespace RentRoom.DTO
{
    public class CreateRoomDTO
    {
        public byte floor { get; set; }
        public Types type { get; set; }

        public decimal Price { get; set; }

        public int RoomNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
