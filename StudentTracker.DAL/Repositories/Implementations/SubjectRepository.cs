using Microsoft.EntityFrameworkCore;
using StudentTracker.DAL.Data;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Repositories.Implementations
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public SubjectRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken = default)
        {
            await _context.Subjects.AddAsync(subject, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSubjectAsync(int id, CancellationToken cancellationToken = default)
        {
            var subject = await _context.Subjects.FindAsync(id, cancellationToken);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync(
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Subjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var subject = await _context.Subjects.FindAsync(id, cancellationToken);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Subject with ID {id} not found.");
            }
            return subject;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByClassRoomIdAsync(int classRoomId, CancellationToken cancellationToken = default)
        {
            return await _context.Subjects
                .Where(s => s.ClassRoomId == classRoomId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateSubjectAsync(Subject subject, CancellationToken cancellationToken = default)
        {
            _context.Subjects.Update(subject);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
