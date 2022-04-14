namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Data.Models.Enums;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersGradesSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UsersGrades.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            if (userManager == null)
            {
                return;
            }

            foreach (var user in userManager.Users)
            {
                foreach (var subject in dbContext.Subjects)
                {
                    await dbContext.UsersGrades.AddRangeAsync(new List<UserGrade>
                    {
                        new UserGrade
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfGrade = DateTime.UtcNow, Grade = 3, GradeType = GradeType.Normal,
                        },
                        new UserGrade
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfGrade = DateTime.UtcNow.AddDays(-1), Grade = 5, GradeType = GradeType.Normal,
                        },
                        new UserGrade
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfGrade = DateTime.UtcNow.AddDays(-2), Grade = 4, GradeType = GradeType.Term,
                        },
                    });
                }
            }
        }
    }
}
