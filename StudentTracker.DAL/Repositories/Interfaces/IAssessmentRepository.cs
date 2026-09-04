using StudentTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface IAssessmentRepository
    {
        Task <IEnumerable<Assessment>> GetAllAssessmentsAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            CancellationToken cancellationToken = default
            );
        Task <IEnumerable<Assessment>> GetAssessmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);

        Task <Assessment> GetAssessmentByIdAsync(int assessmentId, CancellationToken cancellationToken = default);

        Task DeleteAssessmentAsync(int assessmentId, CancellationToken cancellationToken = default);

        Task CreateAssessmentAsync(Assessment assessment, CancellationToken cancellationToken = default);

        Task<IEnumerable<Assessment>> GetAssessmentsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken = default);
    }
}
