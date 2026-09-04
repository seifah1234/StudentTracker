using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? ParentPhoneNumber { get; set; }
        public int ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public DateTime BirthDate { get; set; }

        public string NationalId { get; set; } = string.Empty;

        public DateTime EnrollementDate { get; set; } = DateTime.UtcNow;

        public string PersonalPhotoUrl { get; set; } = string.Empty;

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
    }
}
