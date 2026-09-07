using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;
using StudentTracker.Shared.Enums;

namespace StudentTracker.BLL.Interfaces
{
    public interface IClassRoomService
    {
        Task<IEnumerable<ClassRoom>> GetAllClassRooms(
            Semester? semester = null,
            string? academicYear = null,
            CancellationToken cancellationToken = default);

        Task<ClassRoom?> GetClassRoom(int id, CancellationToken cancellationToken = default);

        Task<ClassRoom> CreateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default);

        Task<ClassRoom> UpdateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default);

        Task<bool> DeleteClassRoom(int id, CancellationToken cancellationToken = default);
        Task<bool> ClassRoomExists(int id, CancellationToken cancellationToken = default);
        Task<List<Student>> GetClassRoomStudents(int id, CancellationToken cancellationToken = default);
    }
}
