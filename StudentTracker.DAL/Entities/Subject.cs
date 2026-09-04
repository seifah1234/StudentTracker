using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int ClassRoomId { get; set; }

        public ClassRoom? ClassRoom { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal MaximumMarks { get; set; }

        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    }
}
