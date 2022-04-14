namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Common;
    using GradeCenter.Server.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSubjectsSeeder : ISeeder
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
                if (await userManager.IsInRoleAsync(user, GlobalConstants.Data.Roles.TeacherRoleName)
                    || await userManager.IsInRoleAsync(user, GlobalConstants.Data.Roles.StudentRoleName))
                {
                    foreach (var subject in dbContext.Subjects)
                    {
                        await dbContext.UsersSubjects.AddAsync(new UserSubject
                        {
                            UserId = user.Id,
                            SubjectId = subject.Id,
                        });
                    }
                }
            }
        }
    }
}
