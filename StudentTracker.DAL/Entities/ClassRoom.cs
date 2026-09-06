using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class ClassRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

        public Semester Semester { get; set; }

        public int ExpectedStudentCount { get; set; }

        public string AcademicYear { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        

    }
}
