namespace RentRoom.DTO
{
    public class RoomFilterDTO
    {
       public byte? floor {  get; set; }
       public int? roomnumber {  get; set; }
       public string? Type { get; set; }
       public decimal? MaxPrice { get; set; }
    }
}
