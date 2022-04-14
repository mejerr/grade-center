namespace GradeCenter.Server.Data.Configurations
{
    using System;

    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserRelationConfiguration : IEntityTypeConfiguration<UserRelation>
    {
        public void Configure(EntityTypeBuilder<UserRelation> userRelation)
        {
            userRelation
                .HasKey(k => new { k.UserInferiorId, k.UserSuperiorId, k.UserRoleId });

            userRelation
                .HasOne(ur => ur.UserRole)
                .WithMany(r => r.UsersRelations)
                .HasForeignKey(ur => ur.UserRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            userRelation
                .HasOne(ur => ur.UserInferior)
                .WithMany(u => u.UsersInferiorRelations)
                .HasForeignKey(ur => ur.UserInferiorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            userRelation
                .HasOne(ur => ur.UserSuperior)
                .WithMany(u => u.UsersSuperiorRelations)
                .HasForeignKey(ur => ur.UserSuperiorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
