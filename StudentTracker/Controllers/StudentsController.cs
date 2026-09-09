using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;

namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            var createdStudent = await _studentService.CreateStudent(student, CancellationToken.None);
            return Ok(createdStudent);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents(CancellationToken.None);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudent(id, CancellationToken.None);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student updatedStudent)
        {
            var id = updatedStudent.Id;
            var student = await _studentService.GetStudent(id, CancellationToken.None);
            if(student == null)
            {
                return NotFound();
            }
            student.Name = updatedStudent.Name;
            student.ParentPhoneNumber = updatedStudent.ParentPhoneNumber;
            student.BirthDate = updatedStudent.BirthDate;
            student.NationalId = updatedStudent.NationalId;
            student.Attendances = updatedStudent.Attendances;
            student.Assessments =updatedStudent.Assessments;
            return Ok(student);
        }

        [HttpGet("total-level/{studentId}")]
        public async Task<IActionResult> GetStudentTotalLevel(int studentId)
        {
            var student = await _studentService.GetStudent(studentId, CancellationToken.None);
            if (student == null)
            {
                return NotFound();
            }
            var totalLevel = _studentService.GetStudentTotalLevel(studentId);
            return Ok(totalLevel);
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> StudentExists(int id)
        {
            var student = await _studentService.GetStudent(id, CancellationToken.None);
            return Ok(student != null);
        }

        [HttpGet("subjectsLevels/{studentId}")]
        public async Task<IActionResult> GetStudentSubjectsLevels(int studentId)
        {
            var student = await _studentService.GetStudent(studentId, CancellationToken.None);
            if (student == null)
            {
                return NotFound();
            }
            var subjectsLevels = await _studentService.GetStudentSubjectsLevel(studentId);
            return Ok(subjectsLevels.Select(s => s.ToString()));
        }
    }
}
