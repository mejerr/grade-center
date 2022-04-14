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

    public class UsersPresences : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UsersPresences.Any())
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
                    await dbContext.UsersPresences.AddRangeAsync(new List<UserPresence>
                    {
                        new UserPresence
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfClass = DateTime.UtcNow, PresenceType = PresenceType.Present,
                        },
                        new UserPresence
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfClass = DateTime.UtcNow.AddDays(-1), PresenceType = PresenceType.Late,
                        },
                        new UserPresence
                        {
                            UserId = user.Id, SubjectId = subject.Id,
                            DateOfClass = DateTime.UtcNow.AddDays(-2), PresenceType = PresenceType.Absent,
                        },
                    });
                }
            }
        }
    }
}
