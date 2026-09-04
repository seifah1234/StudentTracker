using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class Assessment : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public decimal MaximumMarks { get; set; }
        public DateTime Date { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public decimal ObtainedMarks { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
