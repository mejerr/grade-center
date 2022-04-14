namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Common;
    using GradeCenter.Server.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            if (roleManager.Roles.Any())
            {
                return;
            }

            await SeedRoleAsync(roleManager, GlobalConstants.Data.Roles.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.Data.Roles.PrincipalRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.Data.Roles.TeacherRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.Data.Roles.ParentRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.Data.Roles.StudentRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
