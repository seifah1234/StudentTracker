using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;
        private readonly AutoMapper.IMapper mapper;

        public AttendanceService(IAttendanceRepository attendanceRepository, AutoMapper.IMapper mapper)
        {
            this.attendanceRepository = attendanceRepository;
            this.mapper = mapper;
        }

        public async Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendanceDto, CancellationToken cancellationToken = default)
        {
            if (attendanceDto == null)
            {
                throw new ArgumentNullException(nameof(attendanceDto));
            }

            if (attendanceDto.StudentId <= 0)
            {
                throw new ArgumentException("StudentId must be greater than zero.", nameof(attendanceDto.StudentId));
            }

            if (attendanceDto.Date == null)
            {
                throw new ArgumentException("Date cannot be null.", nameof(attendanceDto.Date));
            }

            var attendanceEntity = mapper.Map<StudentTracker.DAL.Entities.Attendance>(attendanceDto);
            await attendanceRepository.CreateAttendanceAsync(attendanceEntity, cancellationToken);
            return attendanceDto;
        }

        public async Task<bool> DeleteAttendanceAsync(int attendanceId, CancellationToken cancellationToken = default)
        {
            if (attendanceId <= 0)
            {
                throw new ArgumentException("AttendanceId must be greater than zero.", nameof(attendanceId));
            }

            await attendanceRepository.DeleteAttendanceAsync(attendanceId, cancellationToken);
            return true;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync(
            int? studentId = null,
            DateTime? date = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? classId = null,
            AttendanceStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            var attendanceEntities = await attendanceRepository.GetAllAttendancesAsync(cancellationToken);
            if (attendanceEntities == null)
            {
                throw new ArgumentException(nameof(attendanceEntities));
            }

            if (studentId != null)
            {
                attendanceEntities = attendanceEntities.Where(x => x.StudentId == studentId);
            }

            if (date != null)
            {
                attendanceEntities = attendanceEntities.Where(x => x.Date.Date == date.Value.Date);
            }

            if (startDate != null && endDate != null)
            {
                attendanceEntities = attendanceEntities.Where(x => x.Date.Date >= startDate.Value.Date && x.Date.Date <= endDate.Value.Date);
            }

            if (classId != null)
            {
                attendanceEntities = attendanceEntities.Where(x => x.Student.ClassRoomId == classId);
            }

            if (status != null)
            {
                attendanceEntities = attendanceEntities.Where(x => x.Status == status);
            }

            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }



        public async Task<AttendanceDto> GetAttendanceByIdAsync(int attendanceId, CancellationToken cancellationToken)
        {
            var attendanceEntity = await attendanceRepository.GetAttendanceByIdAsync(attendanceId);
            if (attendanceEntity == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {attendanceId} not found.");
            }

            var attendanceDto = mapper.Map<AttendanceDto>(attendanceEntity);
            return attendanceDto;
        }



        public async Task<AttendanceDto> UpdateAttendanceAsync(int id, AttendanceDto attendanceDto, CancellationToken cancellationToken)
        {
            var existingAttendance = await attendanceRepository.GetAttendanceByIdAsync(id);
            if (existingAttendance == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }

            mapper.Map(attendanceDto, existingAttendance);
            await attendanceRepository.UpdateAttendanceAsync(existingAttendance);
            return attendanceDto;
        }
    }
}
