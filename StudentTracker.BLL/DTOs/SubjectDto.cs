using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.DTOs
{
    public class SubjectDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public int ClassRoomId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal MaximumMarks { get; set; }
    }
}
