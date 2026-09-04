using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    internal interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachers();

        Task<Teacher?> GetTeacherById(int id);

        Task CreatTeacher(Teacher teacher);

        Task UpdateTeacher(Teacher teacher);

        Task DeleteTeacher(int id);

        Task<bool> ExistsTeacher(int id);
        Task <List<Student>> GetTeacherStudents(int id);
        Task<List<ClassRoom>> GetTeacherClassRooms(int id);
    }
}