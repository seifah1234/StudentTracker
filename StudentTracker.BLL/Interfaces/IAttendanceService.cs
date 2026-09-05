using StudentTracker.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface IAttendanceService 
    {
        Task<IEnumerable<AttendanceDto>> GetAttendanceByStudentIdAsync(int studentId);

        Task<IEnumerable<AttendanceDto>> GetAttendanceByDateAsync(DateTime date);

        Task<IEnumerable<AttendanceDto>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<AttendanceDto> GetAttendanceByIdAsync(int attendanceId);

        Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendanceDto);

        Task<AttendanceDto> UpdateAttendanceAsync(int id,AttendanceDto attendanceDto);

        Task<bool> DeleteAttendanceAsync(int attendanceId);
        
        Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync();

        Task<IEnumerable<AttendanceDto>> GetAttendancesByClassIdAsync(int classId);
    }
}
