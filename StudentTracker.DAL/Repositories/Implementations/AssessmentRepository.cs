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
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public AssessmentRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAssessmentAsync(Assessment assessment, CancellationToken cancellationToken = default)
        {
            await _context.Assessments.AddAsync(assessment, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAssessmentAsync(int assessmentId, CancellationToken cancellationToken = default)
        {
            var assessment = await _context.Assessments.FindAsync(assessmentId, cancellationToken);
            if (assessment != null)
            {
                _context.Assessments.Remove(assessment);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new KeyNotFoundException($"Assessment with ID {assessmentId} not found.");
            }
        }

        public Task<IEnumerable<Assessment>> GetAllAssessmentsAsync(DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Assessment> GetAssessmentByIdAsync(int assessmentId, CancellationToken cancellationToken = default)
        {
            var assessment = await _context.Assessments.FindAsync(assessmentId, cancellationToken);
            if (assessment == null)
            {
                throw new KeyNotFoundException($"Assessment with ID {assessmentId} not found.");
            }
            return assessment;
        }
      

        public async Task<IEnumerable<Assessment>> GetAssessmentsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken = default)
        {
            return await _context.Assessments.Where(a => a.SubjectId == subjectId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _context.Assessments.Where(a => a.StudentId == studentId).ToListAsync(cancellationToken);
        }
    }
}
