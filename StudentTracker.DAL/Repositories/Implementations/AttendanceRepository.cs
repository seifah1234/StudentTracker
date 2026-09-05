using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL.Data;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.DAL.UnitOfWork;
using StudentTracker.Shared.Enums;

namespace StudentTracker.DAL.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public AttendanceRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default)
        {
            await _context.Attendances.AddAsync(attendance, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAttendanceAsync(int id, CancellationToken cancellationToken = default)
        {
            var attendance = await _context.Attendances.FindAsync(id, cancellationToken);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }

        }

        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync(AttendanceStatus? status = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var attendances = _context.Attendances.AsQueryable();

            if (status.HasValue)
            {
                attendances = attendances.Where(a => a.Status == status.Value);
            }

            if (startDate.HasValue)
            {
                attendances = attendances.Where(a => a.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                attendances = attendances.Where(a => a.Date <= endDate.Value);
            }

            return await Task.FromResult(attendances.ToList());
        }

        public async Task<IEnumerable<Attendance>> GetAttendanceByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var attendances = await _context.Attendances.Where(a => a.Date == date).ToListAsync(cancellationToken);
            return attendances;
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var attendance = await _context.Attendances.FindAsync(id, cancellationToken);
            if (attendance == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }
            return attendance;
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByClassIdAsync(int classId, CancellationToken cancellationToken = default)
        {
            var attendances = await _context.Attendances.Include(a => a.Student).Where(a => (a.Student != null) ?  a.Student.ClassRoomId == classId : true).ToListAsync(cancellationToken);
            return attendances;
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var attendances = await _context.Attendances.Where(a => a.StudentId == studentId).ToListAsync(cancellationToken);
            return attendances;
        }

        public async Task UpdateAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default)
        {
            _context.Attendances.Update(attendance);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
