using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;
using StudentTracker.Shared.Enums;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface IClassRoomRepository
    {
        Task<IEnumerable<ClassRoom>> GetAllClassRooms(
            Semester? semester = null,
            string? academicYear = null,
            CancellationToken cancellationToken = default);

        Task<ClassRoom?> GetClassRoom(int id, CancellationToken cancellationToken = default);
        Task<bool> ClassRoomExists(int id, CancellationToken cancellationToken = default);
        Task CreateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default);
        Task UpdateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default);
        Task DeleteClassRoom(int id, CancellationToken cancellationToken = default);
        Task<List<Student>> GetClassRoomStudents(int id, CancellationToken cancellationToken = default);
        
    }
}