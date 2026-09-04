using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.Data.Configurations
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.StudentId, a.SubjectId }).IsUnique();

            builder.HasOne<Subject>()
                .WithMany()
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Student>()
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
