using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentRoom.DbContextProj;
using RentRoom.DTO;
using RentRoom.Interfaces;
using RentRoom.Models;

namespace RentRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomInterface _service;
        private readonly IMemoryCache _memory;

        public RoomController(IRoomInterface service, IMemoryCache memory)
        {
            _service = service;
            _memory = memory;
        }

        [HttpGet("RoomsByFilter")]
        public async Task<IActionResult> GetRooms([FromQuery] RoomFilterDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.IGetRoomByFilter(DTO);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
        [HttpGet("AvaivableRooms")]
        public async Task<IActionResult> GetFreeRooms([FromQuery] FreeRoomDTO Filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var freerooms = await _service.IGetAvaivableRooms(Filter);
            if (freerooms == null)
            {
                return BadRequest("По вашим запросам ничего не найдено");

            }
            return Ok(freerooms);
        }


        [HttpGet("AllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var Rooms = await _service.IGetAllRooms();

            return Ok(Rooms);
        }

        [HttpPost("PostRoom")]
        public async Task<IActionResult> CreateRooms([FromBody] CreateRoomDTO room)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.IPostRoom(room);
            return Ok();
        }


        [HttpDelete("DeleteRoom/{id:int}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            await _service.DeleteRooms(id);
            return NoContent();
        }
        [HttpPut("PutRoom/{id:int}")]
       
        public async Task<IActionResult> PutRoom([FromBody] PutRoomDTO UpdateRoomDto,[FromRoute] int id)
        {
           
           await _service.PutRoom(UpdateRoomDto, id);
           return Ok();
        }


    }
}
