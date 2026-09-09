using StudentTracker.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface IAssessmentService
    {
        Task<AssessmentDto> GetAssessmentByIdAsync(int assessmentId, CancellationToken cancellationToken = default);

        Task<AssessmentDto> AddAssessmentAsync(AssessmentDto assessmentDto, CancellationToken cancellationToken = default);

        Task<AssessmentDto> UpdateAssessmentAsync(int id, AssessmentDto assessmentDto, CancellationToken cancellationToken = default);

        Task DeleteAssessmentAsync(int assessmentId, CancellationToken cancellationToken = default);

        Task<IEnumerable<AssessmentDto>> GetAllAssessmentsAsync(int? studentId = null, int? subjectId = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
    }
}
