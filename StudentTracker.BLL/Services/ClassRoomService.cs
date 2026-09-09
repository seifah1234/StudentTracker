using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.Shared.Enums;
namespace StudentTracker.BLL.Services
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly IClassRoomRepository _classRoomRepository;

        public ClassRoomService(IClassRoomRepository classRoomRepository)
        {
            _classRoomRepository = classRoomRepository;
        }
        public async Task<ClassRoom> CreateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default)
        {
            await _classRoomRepository.CreateClassRoom(classRoom, cancellationToken);
            return classRoom;
        }

        public async Task<bool> DeleteClassRoom(int id, CancellationToken cancellationToken = default)
        {
            var exists = await _classRoomRepository.ClassRoomExists(id, cancellationToken);
            if (!exists) return false;
            await _classRoomRepository.DeleteClassRoom(id, cancellationToken);
            return true;
        }

        public Task<IEnumerable<ClassRoom>> GetAllClassRooms(Semester? semester = null, string? academicYear = null, CancellationToken cancellationToken = default)
        {
            return _classRoomRepository.GetAllClassRooms(semester, academicYear, cancellationToken);
        }

        public Task<ClassRoom?> GetClassRoom(int id, CancellationToken cancellationToken = default)
        {
            return _classRoomRepository.GetClassRoom(id, cancellationToken);
        }

        public async Task<bool> ClassRoomExists(int id, CancellationToken cancellationToken = default)
        {
            var exists = await _classRoomRepository.ClassRoomExists(id, cancellationToken);
            if (!exists) throw new KeyNotFoundException($"ClassRoom with ID {id} not found.");
            return exists;
        }

        public async Task<List<Student>> GetClassRoomStudents(int id, CancellationToken cancellationToken = default)
        {
            return await _classRoomRepository.GetClassRoomStudents(id, cancellationToken);
        }

        public async Task<ClassRoom> UpdateClassRoom(int id,ClassRoom classRoom, CancellationToken cancellationToken = default)
        {
            var exists = await _classRoomRepository.ClassRoomExists(id, cancellationToken);
            if (!exists) throw new KeyNotFoundException($"ClassRoom with ID {classRoom.Id} not found.");
            await _classRoomRepository.UpdateClassRoom(classRoom, cancellationToken);
            return classRoom;
        }
    }
}
