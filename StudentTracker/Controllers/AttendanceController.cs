using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[AttendanceController]")]
    public class AttendanceController : ControllerBase
    {
        private static List<Attendance> attendances = new List<Attendance>();
        [HttpPost("add")]
        public IActionResult AddAttendance([FromBody] Attendance attendance)
        {
            attendances.Add(attendance);
            return Ok(attendance);
        }
        [HttpGet("all")]

        public IActionResult GetAllAttendances()
        {
            return Ok(attendances);
        }
        [HttpGet("{id}")]
        public IActionResult GetAttendanceById(int id)
        {
            var attendance = attendances.FirstOrDefault(s=>s.Id==id);
            if(attendance==null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAttendance([FromBody] Attendance attendance)
        {
            var attendanceId=attendance.Id;
            var attendanceUpdate=attendances.FirstOrDefault(s=>s.Id==attendanceId);
            if(attendanceUpdate==null)
            {
                return NotFound();
            }
            attendanceUpdate.Notes=attendance.Notes;
            attendanceUpdate.Date=attendance.Date;  
            attendanceUpdate.Status=attendance.Status;
            return Ok(attendanceUpdate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAttendance(int id)
        {
            var attendance=attendances.FirstOrDefault(s=>s.Id==id);
            if(attendance==null)
            {
                return NotFound();
            }
            attendances.Remove(attendance);
            return Ok(new { message = "Attendance deleted successfully" });
        }
        [HttpGet("student/{studentId}")]
        public IActionResult GetAttendancesByStudentId(int studentId)
        {
            var studentAttendances = attendances.Where(s => s.StudentId == studentId).ToList();
            return Ok(studentAttendances);
        }
        [HttpGet("date/{date}")]
        public IActionResult GetAttendanceByDateAsync(DateTime date)
        {
            var attendancesByDate = attendances.Where(s => s.Date == date).ToList();
            return Ok(attendancesByDate);
        }
        [HttpGet("classRoom/{classId}")]
        public IActionResult GetAttendancesByClassIdAsync(int classId)
        {
            var attendanceByClass = attendances.Where(s=>s.Id==classId).ToList();
            return Ok(attendanceByClass);
        }
    }
}
