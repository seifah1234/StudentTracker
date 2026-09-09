using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassRoomsController : ControllerBase
    {
        private IClassRoomService classRoomService;

        public ClassRoomsController(IClassRoomService classRoomService)
        {
            this.classRoomService = classRoomService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClassRoom([FromBody]ClassRoom classRoom)
        {
            await classRoomService.CreateClassRoom(classRoom);
            return Ok(classRoom);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassRooms()
        {
            var classRooms = await classRoomService.GetAllClassRooms();
            return Ok(classRooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var classRoom = await classRoomService.GetClassRoom(id);
            if(classRoom==null)
            {
                return NotFound();
            }
            return Ok(classRoom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassRoom(int id, [FromBody] ClassRoom classRoom)
        {
            await classRoomService.UpdateClassRoom(id, classRoom);
            return Ok(classRoom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var deleted = await classRoomService.DeleteClassRoom(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(new { message = "ClassRoom deleted successfully" });
        }

        [HttpGet("students/{classRoomId}")]
        public async Task<IActionResult> GetClassRoomStudents(int classRoomId)
        {
            var students = await classRoomService.GetClassRoomStudents(classRoomId);
            return Ok(students);
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> ExistsClassRoom(int id)
        {
            var classRoom=await classRoomService.ClassRoomExists(id);
            return Ok(classRoom);
        }
    }
}
