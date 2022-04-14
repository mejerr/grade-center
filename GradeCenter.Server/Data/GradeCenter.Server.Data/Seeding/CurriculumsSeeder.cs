namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;

    public class CurriculumsSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Curriculums.Any())
            {
                return;
            }

            await dbContext.Curriculums.AddRangeAsync(
                new List<Curriculum>
                {
                    new Curriculum { ClassId = 1, Term = 1 },
                    new Curriculum { ClassId = 2, Term = 2 },
                    new Curriculum { ClassId = 3, Term = 1 },
                    new Curriculum { ClassId = 4, Term = 2 },
                });
        }
    }
}
