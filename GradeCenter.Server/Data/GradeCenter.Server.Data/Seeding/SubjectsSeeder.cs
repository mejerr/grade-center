namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;

    public class SubjectsSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Subjects.Any())
            {
                return;
            }

            await dbContext.Subjects.AddRangeAsync(
                new List<Subject>
                {
                    new Subject { Name = "Математика" },
                    new Subject { Name = "Български Език" },
                    new Subject { Name = "Английски Език" },
                    new Subject { Name = "Литература" },
                    new Subject { Name = "Информатика" },
                    new Subject { Name = "Биология" },
                    new Subject { Name = "Физика" },
                    new Subject { Name = "Химия" },
                });
        }
    }
}
