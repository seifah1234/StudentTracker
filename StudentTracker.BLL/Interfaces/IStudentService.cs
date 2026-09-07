using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateStudent(Student student, CancellationToken cancellationToken = default);
        Task<Student> UpdateStudent(Student student, CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken = default);
        Task<Student?> GetStudent(int id, CancellationToken cancellationToken = default);
        Task <IEnumerable<Student>> GetStudentsByClassRoomId(int classRoomId, CancellationToken cancellationToken = default);
         Task<bool> StudentExists(int id, CancellationToken cancellationToken = default);
         Task <decimal> GetStudentGrades(int studentId, CancellationToken cancellationToken = default);
    }
}
