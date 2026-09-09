using StudentTracker.DAL.Entities;
using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync(
            CancellationToken cancellationToken = default
            );

        Task <Attendance> GetAttendanceByIdAsync(int id, CancellationToken cancellationToken = default);   

        Task <IEnumerable<Attendance>> GetAttendancesByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);

        Task CreateAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default);

        Task UpdateAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default);

        Task DeleteAttendanceAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Attendance>> GetAttendanceByDateAsync(DateTime date, CancellationToken cancellationToken = default);

        Task<IEnumerable<Attendance>> GetAttendancesByClassIdAsync(int classId, CancellationToken cancellationToken = default);
    }
}
