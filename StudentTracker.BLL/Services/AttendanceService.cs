using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Repositories.Interfaces;
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

        public async Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendanceDto)
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
            await attendanceRepository.CreateAttendanceAsync(attendanceEntity);
            return attendanceDto;
        }

        public async Task<bool> DeleteAttendanceAsync(int attendanceId)
        {
            if (attendanceId <= 0)
            {
                throw new ArgumentException("AttendanceId must be greater than zero.", nameof(attendanceId));
            }

            await attendanceRepository.DeleteAttendanceAsync(attendanceId);
            return true;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync()
        {
            var attendanceEntities = await attendanceRepository.GetAllAttendancesAsync();
            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByDateAsync(DateTime date)
        {
            var attendanceEntities = await attendanceRepository.GetAttendanceByDateAsync(date);
            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendanceEntities = await attendanceRepository.GetAllAttendancesAsync(startDate: startDate, endDate: endDate);
            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int attendanceId)
        {
            var attendanceEntity = await attendanceRepository.GetAttendanceByIdAsync(attendanceId);
            if (attendanceEntity == null)
            {
                throw new KeyNotFoundException($"Attendance with ID {attendanceId} not found.");
            }

            var attendanceDto = mapper.Map<AttendanceDto>(attendanceEntity);
            return attendanceDto;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceByStudentIdAsync(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("StudentId must be greater than zero.", nameof(studentId));
            }
            var attendanceEntities = await attendanceRepository.GetAttendancesByStudentIdAsync(studentId);
            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendancesByClassIdAsync(int classId)
        {
            if (classId <= 0)
            {
                throw new ArgumentException("ClassId must be greater than zero.", nameof(classId));
            }
            var attendanceEntities = await attendanceRepository.GetAttendancesByClassIdAsync(classId);
            return attendanceEntities.Select(x => mapper.Map<AttendanceDto>(x));
        }

        public async Task<AttendanceDto> UpdateAttendanceAsync(int id, AttendanceDto attendanceDto)
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
