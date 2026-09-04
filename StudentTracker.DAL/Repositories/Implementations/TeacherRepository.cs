using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.DAL.Data;
using StudentTracker.DAL.UnitOfWork;

namespace StudentTracker.DAL.Repositories.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
         private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;
         public TeacherRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateTeacher(Teacher teacher, CancellationToken cancellationToken = default)
        {
           await _context.Teachers.AddAsync(teacher);
           await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTeacher(int id, CancellationToken cancellationToken = default)
        {
            var teacher =await _context.Teachers.FindAsync(id, cancellationToken);
            if(teacher!=null){
                _context.Teachers.Remove(teacher);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        
        }

        public async Task<bool> ExistsTeacher(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Teachers.AnyAsync<Teacher>(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers(CancellationToken cancellationToken = default)
        {
            return await _context.Teachers.ToListAsync(cancellationToken);
        }

        public async Task<Teacher?> GetTeacherById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Teachers.FindAsync(id, cancellationToken);
        }

        public async Task<List<Student>> GetTeacherStudents(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                         .Where(s => s.Id == id)
                         .ToListAsync(cancellationToken);
        }

        public async Task UpdateTeacher(Teacher teacher, CancellationToken cancellationToken = default)
        {
            _context.Teachers.Update(teacher);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ClassRoom>> GetTeacherClassRooms(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ClassRooms
                         .Where(cr => cr.TeacherId == id)
                         .ToListAsync(cancellationToken);
        }

    }
}
