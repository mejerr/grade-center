namespace GradeCenter.Server.Data.Configurations
{
    using System;

    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CurriculumSubjectConfiguration : IEntityTypeConfiguration<CurriculumSubject>
    {
        public void Configure(EntityTypeBuilder<CurriculumSubject> curriculumSubject)
        {
            curriculumSubject
                .HasKey(k => new { k.CurriculumId, k.SubjectId });

            curriculumSubject
                .HasOne(cs => cs.Curriculum)
                .WithMany(r => r.CurriculumsSubjects)
                .HasForeignKey(cs => cs.CurriculumId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            curriculumSubject
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.CurriculumsSubjects)
                .HasForeignKey(cs => cs.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
