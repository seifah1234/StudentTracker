using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    internal interface IStudentRepository
    {
         Task<IEnumerable<Student>> GetAllStudents();
         Task<Student?> GetStudent(int id);
         Task<bool> StudentExists(int id);
         Task CreateStudent(Student student);
         Task UpdateStudent(Student student);

    }
}
