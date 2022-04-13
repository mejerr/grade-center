namespace GradeCenter.Server.Data.Configurations
{
    using System;

    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserPresenceConfiguration : IEntityTypeConfiguration<UserPresence>
    {
        public void Configure(EntityTypeBuilder<UserPresence> userPresence)
        {
            userPresence
                .HasKey(k => new { k.UserId, k.SubjectId });

            userPresence
                .HasOne(up => up.User)
                .WithMany(r => r.UsersPresences)
                .HasForeignKey(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            userPresence
                .HasOne(up => up.Subject)
                .WithMany(s => s.UsersPresences)
                .HasForeignKey(up => up.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
