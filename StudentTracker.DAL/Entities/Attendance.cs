using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class Attendance : BaseEntity
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
