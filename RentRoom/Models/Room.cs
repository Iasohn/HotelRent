using System.Reflection.Metadata;
using System.Text;

namespace RentRoom.Models
{

    public enum Types
    {
        Economy,
        Luxe,
        Comfort
    }
    public class Room
    {
        public int Id { get; set; }
        public byte floor { get; set; }
        public Types type { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }
        public List<Booking> Bookings { get; set; }
        public string Image { get; set; }

    }
}
