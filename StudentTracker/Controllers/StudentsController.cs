using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;

namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[StudentsController]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>();

        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            students.Add(student);
            return Ok(student);
        }

        [HttpGet("all")]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s=>s.Id==id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromBody] Student updatedStudent)
        {
            var id = updatedStudent.Id;
            var student=students.FirstOrDefault(s=>s.Id==id);
            if(student==null)
            {
                return NotFound();
            }
            student.Name=updatedStudent.Name;
            student.ParentPhoneNumber=updatedStudent.ParentPhoneNumber;
            student.BirthDate=updatedStudent.BirthDate;
            student.NationalId=updatedStudent.NationalId;
            student.Attendances=updatedStudent.Attendances;
            student.Assessments=updatedStudent.Assessments;
            return Ok(student);
        }

        [HttpGet("grades/{studentId}")]
        public IActionResult GetStudentGrades(int studentId)
        {
            var student = students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.Assessments);
        }
        [HttpGet("exists/{id}")]
        public IActionResult StudentExists(int id)
        {
            var studentExists = students.Any(s=>s.Id==id);
            return Ok(studentExists);
        }
    }
}
