using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        public ICollection<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>();
    }
}
