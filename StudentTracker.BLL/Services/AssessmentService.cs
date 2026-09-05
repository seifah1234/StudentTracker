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
        public async Task<AssessmentDto> AddAssessmentAsync(AssessmentDto assessmentDto)
        {
            if (assessmentDto == null) throw new ArgumentNullException(nameof(assessmentDto));

            if (assessmentDto.StudentId <= 0) throw new ArgumentException("StudentId must be greater than zero.", nameof(assessmentDto.StudentId));

            if (assessmentDto.SubjectId <= 0) throw new ArgumentException("SubjectId must be greater than zero.", nameof(assessmentDto.SubjectId));

            if (string.IsNullOrWhiteSpace(assessmentDto.Name)) throw new ArgumentException("Assessment name cannot be null or empty.", nameof(assessmentDto.Name));

            if (assessmentDto.MaximumMarks <= 0) throw new ArgumentException("Maximum marks must be greater than zero.", nameof(assessmentDto.MaximumMarks));

            if (assessmentDto.ObtainedMarks < 0) throw new ArgumentException("Obtained marks cannot be negative.", nameof(assessmentDto.ObtainedMarks));

            if (assessmentDto.Date == default) throw new ArgumentException("Assessment date must be a valid date.", nameof(assessmentDto.Date));

            var assessmentEntity = mapper.Map<StudentTracker.DAL.Entities.Assessment>(assessmentDto);
            await assessmentRepository.CreateAssessmentAsync(assessmentEntity);
            return assessmentDto;
        }

        public async Task DeleteAssessmentAsync(int assessmentId)
        {
            if (assessmentId <= 0) throw new ArgumentException("AssessmentId must be greater than zero.", nameof(assessmentId));

            var assessmentEntity = await assessmentRepository.GetAssessmentByIdAsync(assessmentId);

            if (assessmentEntity == null) throw new KeyNotFoundException($"Assessment with ID {assessmentId} not found.");

            await assessmentRepository.DeleteAssessmentAsync(assessmentId);
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAssessmentsAsync()
        {
            var assessmentEntities = await assessmentRepository.GetAllAssessmentsAsync();
            return assessmentEntities.Select(mapper.Map<AssessmentDto>);
        }

        public async Task<AssessmentDto> GetAssessmentByIdAsync(int assessmentId)
        {
            var assessmentEntity = await assessmentRepository.GetAssessmentByIdAsync(assessmentId);
            return mapper.Map<AssessmentDto>(assessmentEntity);
        }

        public async Task<IEnumerable<AssessmentDto>> GetAssessmentsByDateAsync(DateTime date)
        {
            var assessmentEntities = await assessmentRepository.GetAssessmentsByDateAsync(date);
            return assessmentEntities.Select(mapper.Map<AssessmentDto>);
        }

        public async Task<IEnumerable<AssessmentDto>> GetAssessmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var assessmentEntities = await assessmentRepository.GetAllAssessmentsAsync(startDate, endDate);
            return assessmentEntities.Select(mapper.Map<AssessmentDto>);
        }

        public async Task<IEnumerable<AssessmentDto>> GetAssessmentsByStudentIdAsync(int studentId)
        {
            var assessmentEntities = await assessmentRepository.GetAssessmentsByStudentIdAsync(studentId);
            return assessmentEntities.Select(mapper.Map<AssessmentDto>);
        }

        public async Task<AssessmentDto> UpdateAssessmentAsync(int id,AssessmentDto assessmentDto)
        {
            if (assessmentDto == null) throw new ArgumentNullException(nameof(assessmentDto));

            if (assessmentDto.StudentId <= 0) throw new ArgumentException("StudentId must be greater than zero.", nameof(assessmentDto.StudentId));

            if (assessmentDto.SubjectId <= 0) throw new ArgumentException("SubjectId must be greater than zero.", nameof(assessmentDto.SubjectId));

            if (string.IsNullOrWhiteSpace(assessmentDto.Name)) throw new ArgumentException("Assessment name cannot be null or empty.", nameof(assessmentDto.Name));

            if (assessmentDto.MaximumMarks <= 0) throw new ArgumentException("Maximum marks must be greater than zero.", nameof(assessmentDto.MaximumMarks));

            if (assessmentDto.ObtainedMarks < 0) throw new ArgumentException("Obtained marks cannot be negative.", nameof(assessmentDto.ObtainedMarks));

            if (assessmentDto.Date == default) throw new ArgumentException("Assessment date must be a valid date.", nameof(assessmentDto.Date));

            var existingAssessment = await assessmentRepository.GetAssessmentByIdAsync(id);
            mapper.Map(assessmentDto, existingAssessment);
            await assessmentRepository.UpdateAssessmentAsync(existingAssessment);

            return assessmentDto;
        }
    }
}
