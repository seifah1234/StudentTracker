using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface IStudentRepository
    {
         Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken = default);
         Task<Student?> GetStudent(int id, CancellationToken cancellationToken = default);
         Task<bool> StudentExists(int id, CancellationToken cancellationToken = default);
         Task CreateStudent(Student student, CancellationToken cancellationToken = default);
         Task UpdateStudent(Student student, CancellationToken cancellationToken = default);

        Task <IEnumerable<Student>> GetStudentsByClassRoomId(int classRoomId, CancellationToken cancellationToken = default);
        Task <IEnumerable<Assessment>> GetStudentAllAssessments(int studentId, CancellationToken cancellationToken = default);

    }
}
