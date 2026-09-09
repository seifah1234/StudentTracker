using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
using StudentTracker.DAL.Repositories.Interfaces;
using StudentTracker.Shared.Enums;

namespace StudentTracker.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public readonly ILevelCalculatorService _levelCalculatorService;

        public StudentService(IStudentRepository studentRepository, ILevelCalculatorService levelCalculatorService)
        {
            _studentRepository = studentRepository;
            _levelCalculatorService = levelCalculatorService;
        }

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

        public async Task<StudentLevel> GetStudentTotalLevel(int studentId, CancellationToken cancellationToken = default)
        {
            var studentExists = await _studentRepository.StudentExists(studentId, cancellationToken);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} was not found.");
            }

            var assessments = await _studentRepository.GetStudentAllAssessments(studentId, cancellationToken);
            decimal totalObtainedGrades = assessments.Sum(a => a.ObtainedMarks);
            decimal totalMaxGrades = assessments.Sum(a => a.MaximumMarks);
            var level = await _levelCalculatorService.CalculateLevelAsync(totalObtainedGrades, totalMaxGrades, cancellationToken);
            return level;
        }


        public Task<bool> StudentExists(int id, CancellationToken cancellationToken)
        {
            return _studentRepository.StudentExists(id, cancellationToken);
        }

        public async Task<IEnumerable<StudentLevel>> GetStudentSubjectsLevel(int studentId, CancellationToken cancellationToken = default)
        {
            var studentExists = await _studentRepository.StudentExists(studentId, cancellationToken);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} was not found.");
            }
            var studentAssessmentsTask = await _studentRepository.GetStudentAllAssessments(studentId, cancellationToken);
            var studentSubjectLevels = studentAssessmentsTask.GroupBy(a => a.SubjectId)
                .Select(g =>
                {
                    var totalObtainedGrades = g.Sum(a => a.ObtainedMarks);
                    var totalMaxGrades = g.Sum(a => a.MaximumMarks);
                    return new
                    {
                        SubjectId = g.Key,
                        TotalObtainedGrades = totalObtainedGrades,
                        TotalMaxGrades = totalMaxGrades
                    };
                })
                .Select(async x =>
                {
                    var level = await _levelCalculatorService.CalculateLevelAsync(x.TotalObtainedGrades, x.TotalMaxGrades, cancellationToken);
                    return level;
                });
            return await Task.WhenAll(studentSubjectLevels);
        }
    }
}
