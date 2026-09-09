using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;

namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            await _teacherService.CreateTeacher(teacher);

            return Ok(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachers();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody]Teacher teacher)
        {
            await _teacherService.UpdateTeacher(id, teacher);
            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacher(id);
            return Ok(new { message = "Teacher deleted successfully" });
        }

        [HttpGet("classroom/{teacherId}")]
        public async Task<IActionResult> GetTeacherClassRooms(int teacherId)
        {
            var teacherClassRooms = await _teacherService.GetTeacherClassRooms(teacherId);
            return Ok(teacherClassRooms);
        }

        [HttpGet("students/{teacherId}")]
        public async Task<IActionResult> GetTeacherStudents(int teacherId)
        {
            var teacherStudents = await _teacherService.GetTeacherStudents(teacherId);
            return Ok(teacherStudents);
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> TeacherExists(int id)
        {
            var teacher = await _teacherService.ExistsTeacher(id);

            return Ok(teacher);
        }
        
    }
}
