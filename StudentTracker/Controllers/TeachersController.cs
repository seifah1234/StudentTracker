using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;

namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[TeachersController]")]
    public class TeachersController : ControllerBase
    {
        private static List<Teacher> teachers = new List<Teacher>();

        [HttpPost("add")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            teachers.Add(teacher);
            return Ok(teacher);
        }
        [HttpGet("all")]
        public IActionResult GetAllTeachers()
        {
            return Ok(teachers);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = teachers.FirstOrDefault(s=>s.Id==id);
            if (teacher==null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTeacher([FromBody]Teacher teacher)
        {
            var teacherId=teacher.Id;
            var existTeacher=teachers.FirstOrDefault(s=>s.Id==teacherId);
            if(existTeacher==null)
            {
                return NotFound();
            }
            existTeacher.Name=teacher.Name;
            existTeacher.PhoneNumber=teacher.PhoneNumber;
            existTeacher.Email=teacher.Email;
            return Ok(existTeacher);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher=teachers.FirstOrDefault(s=>s.Id==id);
            if(teacher==null)
            {
                return NotFound();
            }
            teachers.Remove(teacher);
            return Ok(new { message = "Teacher deleted successfully" });
        }
        [HttpGet("classroom/{classRoomId}")]
        public IActionResult GetTeacherClassRooms(int TeacherId)
        {
            var teacher=teachers.FirstOrDefault(s=>s.Id==TeacherId);
            if(teacher==null)
            {
                return NotFound();
            }
            return Ok(teacher.ClassRooms);
        }
        [HttpGet("students/{teacherId}")]
        public IActionResult GetTeacherStudents(int teacherId)
        {
            List<Student> students = new List<Student>();
            var matchedStudents = students.Where(s => s.Id == teacherId).ToList();
            if(matchedStudents==null)
            {
                return NotFound();
            }       
            return Ok(matchedStudents);    
        }
        [HttpGet("exists/{id}")]
        public IActionResult TeacherExists(int id)
        {
            var teacher=teachers.Any(s=>s.Id==id);
            return Ok(teacher);
        }
        
    }
}
