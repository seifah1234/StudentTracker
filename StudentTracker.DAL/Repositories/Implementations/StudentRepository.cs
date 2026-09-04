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
    internal class StudentRepository : IStudentRepository
    {
         private readonly AppDbContext _context;
         public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateStudent(Student student)
        {
           await _context.Students.AddAsync(student);
           await _context.SaveChangesAsync();
        }

        public async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync<Student>(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }

}
