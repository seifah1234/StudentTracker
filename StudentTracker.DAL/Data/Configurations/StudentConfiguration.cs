using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.NationalId).IsUnique();

            builder.HasIndex(s => s.ParentPhoneNumber).IsUnique();
        }
    }
}
