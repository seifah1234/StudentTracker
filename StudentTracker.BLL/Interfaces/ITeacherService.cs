using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachers(CancellationToken cancellationToken = default);

        Task<Teacher?> GetTeacher(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsTeacher(int id, CancellationToken cancellationToken = default);

        Task<Teacher> CreateTeacher(Teacher teacher, CancellationToken cancellationToken = default);

        Task<Teacher> UpdateTeacher(int id, Teacher teacher, CancellationToken cancellationToken = default);

        Task<bool> DeleteTeacher(int id, CancellationToken cancellationToken = default);

        Task<List<ClassRoom>> GetTeacherClassRooms(int id, CancellationToken cancellationToken = default);

        Task<List<Student>> GetTeacherStudents(int id, CancellationToken cancellationToken = default);
    }
}
