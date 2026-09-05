using StudentTracker.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface IAssessmentService
    {
        Task<IEnumerable<AssessmentDto>> GetAssessmentsByStudentIdAsync(int studentId);

        Task<IEnumerable<AssessmentDto>> GetAssessmentsByDateAsync(DateTime date);

        Task<IEnumerable<AssessmentDto>> GetAssessmentsByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<AssessmentDto> GetAssessmentByIdAsync(int assessmentId);

        Task<AssessmentDto> AddAssessmentAsync(AssessmentDto assessmentDto);

        Task<AssessmentDto> UpdateAssessmentAsync(int id, AssessmentDto assessmentDto);

        Task DeleteAssessmentAsync(int assessmentId);

        Task<IEnumerable<AssessmentDto>> GetAllAssessmentsAsync();
    }
}
