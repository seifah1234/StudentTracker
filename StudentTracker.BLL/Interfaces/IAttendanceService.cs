using StudentTracker.BLL.DTOs;
using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface IAttendanceService 
    {
        Task<AttendanceDto> GetAttendanceByIdAsync(int attendanceId, CancellationToken cancellationToken = default);

        Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendanceDto, CancellationToken cancellationToken = default);

        Task<AttendanceDto> UpdateAttendanceAsync(int id, AttendanceDto attendanceDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAttendanceAsync(int attendanceId, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync(
            int? studentId = null,
            DateTime? date = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? classId = null,
            AttendanceStatus? status = null,
            CancellationToken cancellationToken = default);

    }
}
