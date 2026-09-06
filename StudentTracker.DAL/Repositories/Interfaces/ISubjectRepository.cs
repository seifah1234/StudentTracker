using StudentTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync(
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default);
        Task<Subject> GetSubjectByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken = default);
        Task UpdateSubjectAsync(Subject subject, CancellationToken cancellationToken = default);
        Task DeleteSubjectAsync(int id, CancellationToken cancellationToken = default);
        //Task<IEnumerable<Subject>> GetSubjectsByClassRoomIdAsync(int classRoomId, CancellationToken cancellationToken = default);
    }
}
