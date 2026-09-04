using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entities.Student> Students { get; set; }

        public DbSet<Entities.Teacher> Teachers { get; set; }

        public DbSet<Entities.ClassRoom> ClassRooms { get; set; }

        public DbSet<Entities.Subject> Subjects { get; set; }

        public DbSet<Entities.Assessment> Assessments { get; set; }

        public DbSet<Entities.Attendance> Attendances { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // Override OnModelCreating to apply configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
