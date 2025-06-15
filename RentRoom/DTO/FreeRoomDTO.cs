using System.ComponentModel.DataAnnotations;

namespace RentRoom.DTO
{
    public class FreeRoomDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Range(1,200)]

        public byte? Floor {  get; set; }
    }
}
