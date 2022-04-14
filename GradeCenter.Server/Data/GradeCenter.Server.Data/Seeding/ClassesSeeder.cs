namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;

    public class ClassesSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Classes.Any())
            {
                return;
            }

            await dbContext.Classes.AddRangeAsync(
                new List<Class>
                {
                    new Class { SchoolId = 1, Number = 10, Division = "A" },
                    new Class { SchoolId = 1, Number = 9, Division = "B" },
                    new Class { SchoolId = 2, Number = 10, Division = "A" },
                    new Class { SchoolId = 2, Number = 9, Division = "B" },
                });
        }
    }
}
