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

    public class UsersRelationsSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UsersRelations.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            if (userManager == null)
            {
                return;
            }

            var role = await roleManager?.FindByNameAsync(GlobalConstants.Data.Roles.ParentRoleName)!;
            var studentId = 1;

            for (int i = 1; i <= 3; i++)
            {
                var parent = await userManager.FindByNameAsync($"parent{i}");
                for (int j = 1; j <= 4; j++)
                {
                    var student = await userManager.FindByNameAsync($"student{studentId}");

                    await dbContext.UsersRelations.AddAsync(new UserRelation
                    {
                        UserInferiorId = student.Id, UserSuperiorId = parent.Id,
                        UserRoleId = role.Id,
                    });

                    studentId++;
                }
            }
        }
    }
}
