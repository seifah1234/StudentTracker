using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.DAL.Data;

namespace StudentTracker.DAL.Repositories.Implementations
{
    internal class ClassRoomRepository : IClassRoomRepository
    {
         private readonly AppDbContext _context;
         public ClassRoomRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClassRoom>> GetAllClassRooms()
        {
            return await _context.ClassRooms.ToListAsync();
        }

        public async Task<ClassRoom?> GetClassRoom(int id)
        {
            return await _context.ClassRooms.FindAsync(id);
        }

        public async Task<bool> ClassRoomExists(int id)
        {
            return await _context.ClassRooms.AnyAsync<ClassRoom>(cr => cr.Id == id);
        }

        public async Task CreateClassRoom(ClassRoom classRoom)
        {
            await _context.ClassRooms.AddAsync(classRoom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassRoom(ClassRoom classRoom)
        {
            _context.ClassRooms.Update(classRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassRoom(int id)
        {
            var classRoom = await _context.ClassRooms.FindAsync(id);
            if (classRoom != null)
            {
                _context.ClassRooms.Remove(classRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Student>> GetClassRoomStudents(int id)
        {
            return await _context.Students
                         .Where(s => s.ClassRoomId == id)
                         .ToListAsync();
        }
    }
}