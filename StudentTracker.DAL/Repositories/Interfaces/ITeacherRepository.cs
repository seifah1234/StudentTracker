using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachers(CancellationToken cancellationToken = default);

        Task<Teacher?> GetTeacherById(int id, CancellationToken cancellationToken = default);

        Task CreateTeacher(Teacher teacher, CancellationToken cancellationToken = default);

        Task UpdateTeacher(Teacher teacher, CancellationToken cancellationToken = default);

        Task DeleteTeacher(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsTeacher(int id, CancellationToken cancellationToken = default);
        Task <List<Student>> GetTeacherStudents(int id, CancellationToken cancellationToken = default);
        Task<List<ClassRoom>> GetTeacherClassRooms(int id, CancellationToken cancellationToken = default);
    }
}