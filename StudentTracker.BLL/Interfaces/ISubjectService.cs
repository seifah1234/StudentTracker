using StudentTracker.BLL.DTOs;
using StudentTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Interfaces
{
    public interface ISubjectService
    {
        Task <List<SubjectDto>> GetAllSubjectsAsync(
            int pageNumber = 1,
            int pageSize = 10
            );

        Task<SubjectDto> GetSubjectByIdAsync(int id);

        Task CreateSubjectAsync(SubjectDto subjectDto);

        Task DeleteSubjectAsync(int id);

        Task UpdateSubjectAsync(int id,SubjectDto subjectDto);
    }
}
