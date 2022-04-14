namespace GradeCenter.Server.Data.Configurations
{
    using System;

    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserSubjectConfiguration : IEntityTypeConfiguration<UserSubject>
    {
        public void Configure(EntityTypeBuilder<UserSubject> userSubject)
        {
            userSubject
                .HasKey(k => new { k.UserId, k.SubjectId });

            userSubject
                .HasOne(us => us.User)
                .WithMany(r => r.UsersSubjects)
                .HasForeignKey(us => us.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            userSubject
                .HasOne(us => us.Subject)
                .WithMany(s => s.UsersSubjects)
                .HasForeignKey(us => us.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
