namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider);
    }
}
