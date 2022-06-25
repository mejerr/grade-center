using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeCenter.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradeCenter.Server.Data.Configurations
{
    public class CurriculumTeacherConfiguration : IEntityTypeConfiguration<CurriculumTeacher>
    {
        public void Configure(EntityTypeBuilder<CurriculumTeacher> curriculumTeacher)
        {
            curriculumTeacher
                .HasKey(k => new { k.CurriculumId, k.TeacherId });

            curriculumTeacher
                .HasOne(cs => cs.Curriculum)
                .WithMany(r => r.CurriculumsTeachers)
                .HasForeignKey(cs => cs.CurriculumId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            curriculumTeacher
                .HasOne(cs => cs.Teacher)
                .WithMany(s => s.CurriculumsTeachers)
                .HasForeignKey(cs => cs.TeacherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
