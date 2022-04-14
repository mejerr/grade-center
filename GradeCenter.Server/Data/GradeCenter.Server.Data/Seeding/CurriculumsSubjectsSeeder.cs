namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;

    public class CurriculumsSubjectsSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CurriculumsSubjects.Any())
            {
                return;
            }

            var data = new List<CurriculumSubject>();

            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    data.Add(new CurriculumSubject { CurriculumId = i, SubjectId = j });
                }
            }

            await dbContext.CurriculumsSubjects.AddRangeAsync(data);
        }
    }
}
