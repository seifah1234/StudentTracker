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
    public class StudentRepository : IStudentRepository
    {
         private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;
         public StudentRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateStudent(Student student, CancellationToken cancellationToken = default)
        {
           await _context.Students.AddAsync(student);
           await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> StudentExists(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Students.AnyAsync<Student>(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken = default)
        {
            return await _context.Students.ToListAsync(cancellationToken);
        }

        public async Task<Student?> GetStudent(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Students.FindAsync(id, cancellationToken);
        }

        public async Task UpdateStudent(Student student, CancellationToken cancellationToken = default)
        {
            _context.Students.Update(student);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassRoomId(int classRoomId, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                         .Where(s => s.ClassRoomId == classRoomId)
                         .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Assessment>> GetStudentAllAssessments(int studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Assessments
                .Include(a => a.Subject)
                .Where(a => a.StudentId == studentId)
                .ToListAsync(cancellationToken);
        }


    }

}
