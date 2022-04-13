namespace GradeCenter.Server.Data.Configurations
{
    using System;

    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserGradeConfiguration : IEntityTypeConfiguration<UserGrade>
    {
        public void Configure(EntityTypeBuilder<UserGrade> userGrade)
        {
            userGrade
                .HasKey(k => new { k.UserId, k.SubjectId });

            userGrade
                .HasOne(ug => ug.User)
                .WithMany(r => r.UsersGrades)
                .HasForeignKey(ug => ug.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            userGrade
                .HasOne(ug => ug.Subject)
                .WithMany(s => s.UsersGrades)
                .HasForeignKey(ug => ug.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
