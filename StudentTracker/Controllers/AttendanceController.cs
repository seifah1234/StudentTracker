using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
using StudentTracker.Shared.Enums;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceDto attendance)
        {
            var createdAttendance = await _attendanceService.AddAttendanceAsync(attendance);
            return Ok(createdAttendance);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendances(int? studentId = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, int? classId = null, AttendanceStatus? status = null)
        {
            var attendances = await _attendanceService.GetAllAttendanceAsync(studentId, date, startDate, endDate, classId, status);
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if(attendance==null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromBody] AttendanceDto attendance)
        {
            var attendanceUpdate=await _attendanceService.GetAttendanceByIdAsync(id);

            if(attendanceUpdate==null)
            {
                return NotFound();
            }
            attendanceUpdate.Notes = attendance.Notes;
            attendanceUpdate.Date = attendance.Date;  
            attendanceUpdate.Status = attendance.Status;
            await _attendanceService.UpdateAttendanceAsync(id, attendanceUpdate);
            return Ok(attendanceUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance=await _attendanceService.GetAttendanceByIdAsync(id);
            if(attendance==null)
            {
                return NotFound();
            }
            await _attendanceService.DeleteAttendanceAsync(id);
            return Ok(new { message = "Attendance deleted successfully" });
        }
    }
}
