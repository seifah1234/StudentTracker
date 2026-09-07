using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[ClassRoomsController]")]
    public class ClassRoomsController : ControllerBase
    {
        private static List<ClassRoom> classRooms = new List<ClassRoom>();
        [HttpPost("add")]
        public IActionResult AddClassRoim([FromBody]ClassRoom classRoom)
        {
            classRooms.Add(classRoom);
            return Ok(classRoom);
        }
        [HttpGet("all")]
        public IActionResult GetAllClassRooms()
        {
            return Ok(classRooms);
        }
        [HttpGet("{id}")]
        public IActionResult GetClassRoomById(int id)
        {
            var classRoom = classRooms.FirstOrDefault(s=>s.Id==id);
            if(classRoom==null)
            {
                return NotFound();
            }
            return Ok(classRoom);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClassRoom([FromBody] ClassRoom classRoom)
        {
            var classRoomId=classRoom.Id;
            var classRoomUpdate=classRooms.FirstOrDefault(s=>s.Id==classRoomId);
            if(classRoomUpdate==null)
            {
                return NotFound();
            }
            classRoomUpdate.Name=classRoom.Name;
            classRoomUpdate.TeacherId=classRoom.TeacherId;  
            return Ok(classRoomUpdate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteClassRoom(int id)
        {
            var classRoom=classRooms.FirstOrDefault(s=>s.Id==id);
            if(classRoom==null)
            {
                return NotFound();
            }
            classRooms.Remove(classRoom);
            return Ok(new { message = "ClassRoom deleted successfully" });
        }
        [HttpGet("students/{classRoomId}")]
        public IActionResult GetClassRoomStudents(int classRoomId)
        {
            List<Student> students = new List<Student>();
            var studentList=students.Where(s=>s.ClassRoomId==classRoomId).ToList();
            return Ok(studentList);
        }
        [HttpGet("exists/{id}")]
        public IActionResult ExistsClassRoom(int id)
        {
            var classRoom=classRooms.Any(s=>s.Id==id);
            return Ok(classRoom);
        }
    }
}
