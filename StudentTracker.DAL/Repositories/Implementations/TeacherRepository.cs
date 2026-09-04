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
    internal class TeacherRepository : ITeacherRepository
    {
         private readonly AppDbContext _context;
         public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreatTeacher(Teacher teacher)
        {
           await _context.Teachers.AddAsync(teacher);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int id)
        {
            var teacher =await _context.Teachers.FindAsync(id);
            if(teacher!=null){
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        
        }

        public async Task<bool> ExistsTeacher(int id)
        {
            return await _context.Teachers.AnyAsync<Teacher>(t => t.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetTeacherById(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<List<Student>> GetTeacherStudents(int id)
        {
            return await _context.Students
                         .Where(s => s.Id == id)
                         .ToListAsync();
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClassRoom>> GetTeacherClassRooms(int id)
        {
            return await _context.ClassRooms
                         .Where(cr => cr.TeacherId == id)
                         .ToListAsync();
        }

    }
}
