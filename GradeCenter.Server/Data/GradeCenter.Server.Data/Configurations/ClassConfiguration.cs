namespace GradeCenter.Server.Data.Configurations
{
    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> classEntity)
        {
            classEntity
                .HasOne(c => c.School)
                .WithMany(s => s.Classes)
                .HasForeignKey(c => c.SchoolId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            classEntity
                .HasMany(c => c.Users)
                .WithOne(u => u.Class)
                .HasForeignKey(u => u.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            classEntity
                .HasMany(c => c.Curriculums)
                .WithOne(cu => cu.Class)
                .HasForeignKey(c => c.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
