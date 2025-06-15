using RentRoom.DTO;
using RentRoom.Models;

namespace RentRoom.Interfaces
{
    public interface IRoomInterface 
    {
        public Task<List<Room>> IGetRoomByFilter(RoomFilterDTO dto);
        public Task IPostRoom(CreateRoomDTO room);
        public Task<List<Room>> IGetAvaivableRooms(FreeRoomDTO dto);

        public Task<List<Room>> IGetAllRooms();
        public Task DeleteRooms(int id);
        public Task PutRoom(PutRoomDTO dto,int id);

        
    }
}
