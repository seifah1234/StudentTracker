using System;
using System.Collections.Generic;
using System.Text;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;

namespace StudentTracker.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<Teacher> CreateTeacher(Teacher teacher, CancellationToken cancellationToken)
        {
            await _teacherRepository.CreateTeacher(teacher, cancellationToken);
            return teacher;
        }

        public async Task<bool> DeleteTeacher(int id, CancellationToken cancellationToken)
        {
            var teacherExists = await _teacherRepository.ExistsTeacher(id, cancellationToken);
            if (!teacherExists) return false;
            await _teacherRepository.DeleteTeacher(id, cancellationToken);
            return true;
        }

        public Task<bool> ExistsTeacher(int id, CancellationToken cancellationToken)
        {
            return _teacherRepository.ExistsTeacher(id, cancellationToken);
        }

        Task<IEnumerable<Teacher>> ITeacherService.GetAllTeachers(CancellationToken cancellationToken)
        {
            return _teacherRepository.GetAllTeachers(cancellationToken);
        }

        Task<Teacher?> ITeacherService.GetTeacher(int id, CancellationToken cancellationToken)
        {
            var teacherExists = _teacherRepository.ExistsTeacher(id, cancellationToken);
            if (!teacherExists.Result) throw new KeyNotFoundException($"Teacher with ID {id} not found");   
            return _teacherRepository.GetTeacherById(id, cancellationToken);
        }

        Task<List<ClassRoom>> ITeacherService.GetTeacherClassRooms(int id, CancellationToken cancellationToken)
        {
            return _teacherRepository.GetTeacherClassRooms(id, cancellationToken);
        }

        Task<List<Student>> ITeacherService.GetTeacherStudents(int id, CancellationToken cancellationToken)
        {
            return _teacherRepository.GetTeacherStudents(id, cancellationToken);
        }

        async Task<Teacher> ITeacherService.UpdateTeacher(Teacher teacher, CancellationToken cancellationToken)
        {
            var teacherExists = await _teacherRepository.ExistsTeacher(teacher.Id, cancellationToken);
            if (!teacherExists) throw new KeyNotFoundException($"Teacher with ID {teacher.Id} not found");
            await _teacherRepository.UpdateTeacher(teacher, cancellationToken);
            return teacher;
        }
    }
}
