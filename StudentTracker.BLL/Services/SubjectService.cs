using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly AutoMapper.IMapper mapper;

        public SubjectService(ISubjectRepository subjectRepository, AutoMapper.IMapper mapper)
        {
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
        }

        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            if (subjectDto == null)
            {
                throw new ArgumentNullException(nameof(subjectDto));
            }

            if (string.IsNullOrWhiteSpace(subjectDto.Name))
            {
                throw new ArgumentException("Subject name cannot be null or empty.", nameof(subjectDto.Name));
            }

            if (subjectDto.MaximumMarks <= 0)
            {
                throw new ArgumentException("Maximum marks must be greater than zero.", nameof(subjectDto.MaximumMarks));
            }

            if (subjectDto.ClassRoomId <= 0)
            {
                throw new ArgumentException("ClassRoomId must be greater than zero.", nameof(subjectDto.ClassRoomId));
            }

            if (subjectDto.Description != null && subjectDto.Description.Length > 500)
            {
                throw new ArgumentException("Description cannot exceed 500 characters.", nameof(subjectDto.Description));
            }

            if (string.IsNullOrEmpty(subjectDto.Code))
            {
                throw new ArgumentException("Subject code cannot be null or empty.", nameof(subjectDto.Code));
            }

            var subject = mapper.Map<DAL.Entities.Subject>(subjectDto);
            await subjectRepository.AddSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            await subjectRepository.DeleteSubjectAsync(id);
        }

        public async Task<List<SubjectDto>> GetAllSubjectsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var subjects = await subjectRepository.GetAllSubjectsAsync(pageNumber: pageNumber, pageSize: pageSize);
            return subjects.Select(mapper.Map<SubjectDto>).ToList();
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(int id)
        {
            var subject = await subjectRepository.GetSubjectByIdAsync(id);
            return mapper.Map<SubjectDto>(subject);
        }

        public async Task UpdateSubjectAsync(int id, SubjectDto subjectDto)
        {
            if (subjectDto == null)
            {
                throw new ArgumentNullException(nameof(subjectDto));
            }

            var subject = await subjectRepository.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                throw new ArgumentException("Subject not found.", nameof(id));
            }

            mapper.Map(subjectDto, subject);
            await subjectRepository.UpdateSubjectAsync(subject);
        }
    }
}
