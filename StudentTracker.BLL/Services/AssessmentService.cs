using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository assessmentRepository;
        private readonly AutoMapper.IMapper mapper;

        public AssessmentService(IAssessmentRepository assessmentRepository, AutoMapper.IMapper mapper)
        {
            this.assessmentRepository = assessmentRepository;
            this.mapper = mapper;
        }

        public async Task<AssessmentDto> AddAssessmentAsync(AssessmentDto assessmentDto, CancellationToken cancellationToken = default)
        {
            if (assessmentDto == null) throw new ArgumentNullException(nameof(assessmentDto));

            if (assessmentDto.StudentId <= 0) throw new ArgumentException("StudentId must be greater than zero.", nameof(assessmentDto.StudentId));

            if (assessmentDto.SubjectId <= 0) throw new ArgumentException("SubjectId must be greater than zero.", nameof(assessmentDto.SubjectId));

            if (string.IsNullOrWhiteSpace(assessmentDto.Name)) throw new ArgumentException("Assessment name cannot be null or empty.", nameof(assessmentDto.Name));

            if (assessmentDto.MaximumMarks <= 0) throw new ArgumentException("Maximum marks must be greater than zero.", nameof(assessmentDto.MaximumMarks));

            if (assessmentDto.ObtainedMarks < 0) throw new ArgumentException("Obtained marks cannot be negative.", nameof(assessmentDto.ObtainedMarks));

            if (assessmentDto.Date == default) throw new ArgumentException("Assessment date must be a valid date.", nameof(assessmentDto.Date));

            var assessmentEntity = mapper.Map<StudentTracker.DAL.Entities.Assessment>(assessmentDto);
            await assessmentRepository.CreateAssessmentAsync(assessmentEntity, cancellationToken);
            return assessmentDto;
        }

        public async Task DeleteAssessmentAsync(int assessmentId, CancellationToken cancellationToken = default )
        {
            if (assessmentId <= 0) throw new ArgumentException("AssessmentId must be greater than zero.", nameof(assessmentId));

            var assessmentEntity = await assessmentRepository.GetAssessmentByIdAsync(assessmentId, cancellationToken);

            if (assessmentEntity == null) throw new KeyNotFoundException($"Assessment with ID {assessmentId} not found.");

            await assessmentRepository.DeleteAssessmentAsync(assessmentId, cancellationToken);
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAssessmentsAsync(int? studentId = null, int? subjectId = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var assessmentEntities = await assessmentRepository.GetAllAssessmentsAsync(cancellationToken);
            if (studentId.HasValue)
            {
                assessmentEntities = assessmentEntities.Where(a => a.StudentId == studentId.Value);
            }
            if (subjectId.HasValue)
            {
                assessmentEntities = assessmentEntities.Where(a => a.SubjectId == subjectId.Value);
            }
            if (date.HasValue)
            {
                assessmentEntities = assessmentEntities.Where(a => a.Date == date.Value);
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                assessmentEntities = assessmentEntities.Where(a => a.Date >= startDate.Value && a.Date <= endDate.Value);
            }

            return assessmentEntities.Select(mapper.Map<AssessmentDto>);
        }

        public async Task<AssessmentDto> GetAssessmentByIdAsync(int assessmentId, CancellationToken cancellationToken = default)
        {
            var assessmentEntity = await assessmentRepository.GetAssessmentByIdAsync(assessmentId, cancellationToken);
            return mapper.Map<AssessmentDto>(assessmentEntity);
        }

        public async Task<AssessmentDto> UpdateAssessmentAsync(int id,AssessmentDto assessmentDto, CancellationToken cancellationToken = default)
        {
            if (assessmentDto == null) throw new ArgumentNullException(nameof(assessmentDto));

            if (assessmentDto.StudentId <= 0) throw new ArgumentException("StudentId must be greater than zero.", nameof(assessmentDto.StudentId));

            if (assessmentDto.SubjectId <= 0) throw new ArgumentException("SubjectId must be greater than zero.", nameof(assessmentDto.SubjectId));

            if (string.IsNullOrWhiteSpace(assessmentDto.Name)) throw new ArgumentException("Assessment name cannot be null or empty.", nameof(assessmentDto.Name));

            if (assessmentDto.MaximumMarks <= 0) throw new ArgumentException("Maximum marks must be greater than zero.", nameof(assessmentDto.MaximumMarks));

            if (assessmentDto.ObtainedMarks < 0) throw new ArgumentException("Obtained marks cannot be negative.", nameof(assessmentDto.ObtainedMarks));

            if (assessmentDto.Date == default) throw new ArgumentException("Assessment date must be a valid date.", nameof(assessmentDto.Date));

            var existingAssessment = await assessmentRepository.GetAssessmentByIdAsync(id, cancellationToken);
            mapper.Map(assessmentDto, existingAssessment);
            await assessmentRepository.UpdateAssessmentAsync(existingAssessment, cancellationToken);

            return assessmentDto;
        }
    }
}
