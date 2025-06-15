using RentRoom.Models;

namespace RentRoom.DTO
{
    public class PutRoomDTO
    {
        public Types Type { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }

        public string Image { get; set; }
    }
}
