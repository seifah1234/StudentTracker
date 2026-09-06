using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Implementations;
using StudentTracker.DAL.Repositories.Interfaces;

namespace StudentTracker.BLL.Services
{
    public class StudentService(IStudentRepository studentRepository) : IStudentService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        public readonly ILevelCalculatorService _levelCalculatorService;

        public async Task<Student> CreateStudent(Student student, CancellationToken cancellationToken)
        {
            await _studentRepository.CreateStudent(student, cancellationToken);
            return student;
        }

        public async Task<Student> UpdateStudent(Student student, CancellationToken cancellationToken)
        {
            var studentExists = await _studentRepository.StudentExists(student.Id, cancellationToken);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} was not found.");
            }

            await _studentRepository.UpdateStudent(student, cancellationToken);
            return student;
        }

        public Task<IEnumerable<Student>> GetAllStudents(CancellationToken cancellationToken)
        {
            return _studentRepository.GetAllStudents(cancellationToken);
        }

        public async Task<Student?> GetStudent(int id, CancellationToken cancellationToken)
        {
            var studentExists = await _studentRepository.StudentExists(id, cancellationToken);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {id} was not found.");
            }
            
            return await _studentRepository.GetStudent(id, cancellationToken);
        }

        public Task<IEnumerable<Student>> GetStudentsByClassRoomId(int classRoomId, CancellationToken cancellationToken)
        {
            return _studentRepository.GetStudentsByClassRoomId(classRoomId, cancellationToken);
        }

        public async Task<decimal> GetStudentGrades(int studentId, CancellationToken cancellationToken = default)
        {
            var studentExists = await _studentRepository.StudentExists(studentId, cancellationToken);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} was not found.");
            }

            var grades = await _studentRepository.GetStudentGrades(studentId, cancellationToken);
            decimal totalGrades = grades.Sum();
            var level = await _levelCalculatorService.CalculateLevelAsync(totalGrades, cancellationToken);
            return totalGrades;
        }


        public Task<bool> StudentExists(int id, CancellationToken cancellationToken)
        {
            return _studentRepository.StudentExists(id, cancellationToken);
        }
    }
}
