namespace RentRoom.Models
{

    public enum IsConfirmed
    {
        NotConfirmed,
        Confirmed,
        Rejected
    }
    public class Booking
    {

        public int Id { get; set; }

        public int RoomId { get; set; }
        public User RentUser { get; set; }
        public string RentUserID { get; set; }

        public Room RentRoom { get; set; }

        public int RoomNumber { get; set; }


        public DateTime RentTime { get; set; } = DateTime.Now;
        
        public DateTime ExpTime { get; set; }

        public IsConfirmed Confirmation { get; set; } = IsConfirmed.NotConfirmed;

    }
}
