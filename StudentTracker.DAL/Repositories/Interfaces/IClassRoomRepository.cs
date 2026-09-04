using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.DAL.Entities;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    internal interface IClassRoomRepository
    {
        Task<IEnumerable<ClassRoom>> GetAllClassRooms();
        Task<ClassRoom?> GetClassRoom(int id);
        Task<bool> ClassRoomExists(int id);
        Task CreateClassRoom(ClassRoom classRoom);
        Task UpdateClassRoom(ClassRoom classRoom);
        Task DeleteClassRoom(int id);
        Task<List<Student>> GetClassRoomStudents(int id);
        
    }
}