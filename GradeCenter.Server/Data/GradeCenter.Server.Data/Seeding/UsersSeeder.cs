namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Common;
    using GradeCenter.Server.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        private readonly PasswordHasher<ApplicationUser> passwordHasher = new();

        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            if (userManager == null)
            {
                return;
            }

            if (userManager.Users.Any())
            {
                return;
            }

            var userStore = new UserStore<ApplicationUser>(dbContext);

            // ADMINISTRATOR
            await this.CreateUser(
                dbContext,
                serviceProvider,
                userStore,
                userManager,
                "admin",
                "123456",
                "admin@gmail.com",
                "AdminFirst AdminLast",
                "гр. Град, кв. Квартал, ул. Улица",
                new DateTime(1999, 1, 1),
                GlobalConstants.Data.Roles.AdministratorRoleName);

            // Principal Math School
            await this.CreateUser(
                dbContext,
                serviceProvider,
                userStore,
                userManager,
                "principalMathSchool",
                "123456",
                "principalMath@gmail.com",
                "PrincipalMathFirst PrincipalMathLast",
                "гр. Град, кв. Квартал, ул. Улица",
                new DateTime(1998, 1, 1),
                GlobalConstants.Data.Roles.PrincipalRoleName);

            // Principal Language School
            await this.CreateUser(
                dbContext,
                serviceProvider,
                userStore,
                userManager,
                "principalLanguageSchool",
                "123456",
                "principalLanguage@gmail.com",
                "PrincipalLanguageFirst PrincipalLanguageLast",
                "гр. Град, кв. Квартал, ул. Улица",
                new DateTime(1997, 1, 1),
                GlobalConstants.Data.Roles.PrincipalRoleName);

            // Seed Teachers
            for (int i = 1; i <= 8; i++)
            {
                await this.CreateUser(
                    dbContext,
                    serviceProvider,
                    userStore,
                    userManager,
                    $"teacher{i}",
                    "123456",
                    $"teacher{i}@gmail.com",
                    $"Teacher{i}First Teacher{i}Last",
                    "гр. Град, кв. Квартал, ул. Улица",
                    new DateTime(1997 - i, 1, 1),
                    GlobalConstants.Data.Roles.TeacherRoleName);
            }

            // Seed Parents
            for (int i = 1; i <= 3; i++)
            {
                await this.CreateUser(
                    dbContext,
                    serviceProvider,
                    userStore,
                    userManager,
                    $"parent{i}",
                    "123456",
                    $"parent{i}@gmail.com",
                    $"Parent{i}First Parent{i}Last",
                    "гр. Град, кв. Квартал, ул. Улица",
                    new DateTime(1989 - i, 1, 1),
                    GlobalConstants.Data.Roles.ParentRoleName);
            }

            // Seed Students
            for (int i = 1; i <= 12; i++)
            {
                await this.CreateUser(
                    dbContext,
                    serviceProvider,
                    userStore,
                    userManager,
                    $"student{i}",
                    "123456",
                    $"student{i}@gmail.com",
                    $"Student{i}First Student{i}Last",
                    "гр. Град, кв. Квартал, ул. Улица",
                    new DateTime(1986 - i, 1, 1),
                    GlobalConstants.Data.Roles.StudentRoleName,
                    i % 4 == 0 ? 4 : i % 4);
            }
        }

        private async Task CreateUser(
            GradeCenterDbContext dbContext,
            IServiceProvider serviceProvider,
            UserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            string userName,
            string password,
            string email,
            string fullName,
            string address,
            DateTime dateOfBirth,
            string roleName,
            int? classId = null)
        {
            var appUser = new ApplicationUser
            {
                FullName = fullName,
                Address = address,
                DateOfBirth = dateOfBirth,
                ClassId = classId,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            if (!dbContext.Users.Any(u => u.UserName == appUser.UserName))
            {
                var hashed = this.passwordHasher.HashPassword(appUser, password);
                appUser.PasswordHash = hashed;

                await userStore.CreateAsync(appUser);
            }

            var user = await userManager.FindByEmailAsync(email);
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
