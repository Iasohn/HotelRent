using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client;
using RentRoom.DbContextProj;
using RentRoom.DTO;
using RentRoom.Interfaces;
using RentRoom.Models;
using System.Runtime.CompilerServices;

namespace RentRoom.Repository
{
    public class RoomRepository : IRoomInterface
    {
        private readonly ProjectDbContext _context;
 
        public RoomRepository(ProjectDbContext context)
        {
            _context = context;
 
        }

        public async Task DeleteRooms(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> IGetAllRooms()
        {
            var rooms = await _context.Rooms.AsNoTracking().ToListAsync();
            return rooms;
        }

        public async Task<List<Room>> IGetAvaivableRooms(FreeRoomDTO dto)
        {
            var queryRoom = _context.Rooms.AsQueryable();

            if (dto.Floor != null)
            {
                queryRoom = _context.Rooms.Where(p => p.floor == dto.Floor);
            }

            var bookedRoomId = _context.Bookings
                .Where(b =>
                    (dto.StartTime >= b.RentTime && dto.StartTime < b.ExpTime) ||
                    (dto.EndTime >= b.RentTime && dto.EndTime <= b.ExpTime) ||
                    (dto.StartTime <= b.RentTime && dto.EndTime >= b.ExpTime))
                .Select(b => b.RentRoom.Id)
                .ToList();
            var availableRooms = await queryRoom.Where(r => !bookedRoomId.Contains(r.Id))
                .ToListAsync();

            if (!availableRooms.Any())
            {
                throw new ArgumentException("К сожалению в такие сроки нету свободных комнат ");
            }

            return availableRooms;
        }

        public async Task<List<Room>> IGetRoomByFilter(RoomFilterDTO dto)
        {
            var queryRoom = _context.Rooms.AsQueryable();

            if (dto.MaxPrice > 0)
            {
                queryRoom = queryRoom.Where(c => c.Price <= dto.MaxPrice);
            }

            if (Enum.TryParse<Types>(dto.Type, ignoreCase: true, out var typeValue) && Enum.IsDefined(typeof(Types), typeValue))
            {
                queryRoom = queryRoom.Where(x => x.type == typeValue);
            }

            if (dto.floor != null)
            {
                queryRoom = queryRoom.Where(c => c.floor == dto.floor);
            }
            if(dto.roomnumber > 0)
            {
                queryRoom = queryRoom.Where(c => c.RoomNumber == dto.roomnumber);
            }
            return await queryRoom.ToListAsync();
        }
        
        public async Task IPostRoom(CreateRoomDTO room)
        {
            if(room == null)
            {

                throw new ArgumentNullException(nameof(room));

            }

            var NewRoom = new Room()
            {
                Price = room.Price,
                RoomNumber = room.RoomNumber,
                floor = room.floor,
                Image = room.ImageUrl,
                type = room.type
            };

            await _context.Rooms.AddAsync(NewRoom);
            await _context.SaveChangesAsync();

        }

        public async Task PutRoom(PutRoomDTO dto, int id)
        {
            var previous = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (previous == null)
            {
                throw new ArgumentException("Error not founded room",nameof(id));
            }

            previous.RoomNumber = dto.RoomNumber;
            previous.Image = dto.Image;
            previous.Price = dto.Price;
            previous.type = dto.Type;

            await _context.SaveChangesAsync();
        }
    }
}
