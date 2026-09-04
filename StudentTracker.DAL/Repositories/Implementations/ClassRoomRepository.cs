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
using StudentTracker.Shared.Enums;

namespace StudentTracker.DAL.Repositories.Implementations
{
    public class ClassRoomRepository : IClassRoomRepository
    {
         private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;
         public ClassRoomRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;   
        }
        public async Task<IEnumerable<ClassRoom>> GetAllClassRooms(
            Semester? semester = null,
            string? academicYear = null,
            CancellationToken cancellationToken = default)
        {
            var classRooms = _context.ClassRooms.AsQueryable();

            if (semester.HasValue)
            {
                classRooms = classRooms.Where(cr => cr.Semester == semester.Value);
            }

            if (!string.IsNullOrEmpty(academicYear))
            {
                classRooms = classRooms.Where(cr => cr.AcademicYear == academicYear);
            }

            return await classRooms.ToListAsync(cancellationToken);
        }

        public async Task<ClassRoom?> GetClassRoom(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ClassRooms.FindAsync(id, cancellationToken);
        }

        public async Task<bool> ClassRoomExists(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ClassRooms.AnyAsync<ClassRoom>(cr => cr.Id == id);
        }

        public async Task CreateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default)
        {
            await _context.ClassRooms.AddAsync(classRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateClassRoom(ClassRoom classRoom, CancellationToken cancellationToken = default)
        {
            _context.ClassRooms.Update(classRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteClassRoom(int id, CancellationToken cancellationToken = default)
        {
            var classRoom = await _context.ClassRooms.FindAsync(id, cancellationToken);
            if (classRoom != null)
            {
                _context.ClassRooms.Remove(classRoom);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Student>> GetClassRoomStudents(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Students
                         .Where(s => s.ClassRoomId == id)
                         .ToListAsync(cancellationToken);
        }
    }
}