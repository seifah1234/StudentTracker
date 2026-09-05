using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.DTOs
{
    public class AssessmentDto
    {
        public string Name { get; set; } = string.Empty;

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public string StudentName { get; set; }

        public int StudentId { get; set; }

        public decimal MaximumMarks { get; set; }

        public decimal ObtainedMarks { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}
