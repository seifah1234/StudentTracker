using StudentTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.DTOs
{
    public class AttendanceDto
    {
        public DateTime? Date { get; set; }

        public string? StudentName { get; set; } = null;

        public string? Status { get; set; } = null;
        public string? Notes { get; set; } = null;

        public int StudentId { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
