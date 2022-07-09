namespace GradeCenter.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Common.Enums;
    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class UserService : IUserService
    {
        private readonly GradeCenterDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserService(
            GradeCenterDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> SetRoleAsync(string userId, string roleId)
        {
            var result = await this.ClearRolesAsync(userId);
            if (!result)
            {
                return null;
            }

            var userRole = new IdentityUserRole<string>()
            {
                UserId = userId,
                RoleId = roleId,
            };

            await this.dbContext.UserRoles.AddAsync(userRole);
            await this.dbContext.SaveChangesAsync();

            return userId;
        }

        public async Task<bool> ClearRolesAsync(string userId)
        {
            var userRoles = await this.dbContext
                .UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync();

            if (!userRoles.Any())
            {
                return false;
            }

            this.dbContext.UserRoles.RemoveRange(userRoles);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<string> AddStudentToClassAsync(string userId, int classId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            user.ClassId = classId;

            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();

            return userId;
        }

        public async Task<string> RemoveStudentFromClassAsync(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            user.ClassId = null;

            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();

            return userId;
        }

        public async Task<string> GetSchoolPrincipalIdAsync(string userId, int schoolId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            if (await this.userManager.IsInRoleAsync(user, PrincipalRoleName))
            {
                return userId;
            }

            return null;
        }

        public async Task<bool> IsTeacherAsync(string userId)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            return await this.userManager.IsInRoleAsync(user, TeacherRoleName);
        }

        public async Task<bool> IsStudentAsync(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            return await this.userManager.IsInRoleAsync(user, StudentRoleName);
        }

        public async Task<bool> IsIsParentAsync(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            return await this.userManager.IsInRoleAsync(user, ParentRoleName);
        }

        public async Task<bool> RemoveUserSubjectAsync(string userId, int subjectId)
        {
            var userSubject =
                await this.dbContext.UsersSubjects
                    .FirstOrDefaultAsync(us =>
                    us.UserId == userId && us.SubjectId == subjectId);
            if (userSubject == null)
            {
                return false;
            }

            this.dbContext.UsersSubjects.Remove(userSubject);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveParentRelationsAsync(string userId)
        {
            var relations = await this.dbContext
                .UsersRelations
                .Where(u =>
                    u.UserSuperiorId == userId &&
                    u.UserRole.Name == ParentRoleName)
                .ToListAsync();

            if (!relations.Any())
            {
                return false;
            }

            this.dbContext.UsersRelations.RemoveRange(relations);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveDependentsAsync(string userSuperiorId, string userInferiorId, string role)
        {
            var relations = await this.dbContext
                .UsersRelations
                .Where(u =>
                    u.UserSuperiorId == userSuperiorId &&
                    u.UserInferiorId == userInferiorId &&
                    u.UserRole.Name == role)
                .ToListAsync();

            if (!relations.Any())
            {
                return false;
            }

            this.dbContext.UsersRelations.RemoveRange(relations);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddDependentAsync(string userSuperiorId, string userInferiorId, string roleId)
        {
            var userRelation = new UserRelation()
            {
                UserSuperiorId = userSuperiorId,
                UserInferiorId = userInferiorId,
                UserRoleId = roleId,
            };

            await this.dbContext.UsersRelations.AddAsync(userRelation);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetTeachersAsync(int? schoolId = null)
        {
            var users = new List<ApplicationUser>();
            foreach (var user in this.userManager.Users)
            {
                if (await this.IsTeacherAsync(user.Id))
                {
                    users.Add(user);
                }
            }

            if (schoolId != null)
            {
                return users.Where(u => u.SchoolId == schoolId);
            }

            return users;
        }

        public async Task<IEnumerable<ApplicationUser>> GetStudentsAsync(int? schoolId = null)
        {
            var users = new List<ApplicationUser>();
            foreach (var user in this.userManager.Users)
            {
                if (await this.IsStudentAsync(user.Id))
                {
                    users.Add(user);
                }
            }

            if (schoolId != null)
            {
                return users.Where(u => u.SchoolId == schoolId);
            }

            return users;
        }

        public async Task<IEnumerable<ApplicationUser>> GetParentsAsync(int? schoolId = null)
        {
            var users = new List<ApplicationUser>();
            foreach (var user in this.userManager.Users)
            {
                if (await this.IsIsParentAsync(user.Id))
                {
                    users.Add(user);
                }
            }

            if (schoolId != null)
            {
                return users.Where(u => u.SchoolId == schoolId);
            }

            return users;
        }

        public async Task<T> GetByNameAsync<T>(string fullName, UserRolesEnum role)
        {
            var userRole = await this.GetRoleByUserRoleEnumAsync(role);

            var user = await this.dbContext.Users
                .Where(u =>
                    u.FullName == fullName &&
                    u.Roles.Any(r => r.RoleId == userRole.Id))
                .To<T>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<T> GetByIdAsync<T>(string id, UserRolesEnum role)
        {
            var userRole = await this.GetRoleByUserRoleEnumAsync(role);

            var user = await this.dbContext.Users
                .Where(u =>
                    u.Id == id &&
                    u.Roles.Any(r => r.RoleId == userRole.Id))
                .To<T>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<T>> GetUserDependendsAsync<T>(string id, UserRolesEnum role)
        {
            var userRole = await this.GetRoleByUserRoleEnumAsync(role);

            var users = await this.dbContext.Users
                .Where(u => u.UsersInferiorRelations
                    .Any(uir => uir.UserSuperiorId == id
                                && uir.UserRole == userRole))
                .To<T>()
                .ToListAsync();

            return users;
        }

        private async Task<ApplicationRole> GetRoleByUserRoleEnumAsync(UserRolesEnum role)
        {
            switch (role)
            {
                case UserRolesEnum.Administrator:
                    return await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == AdministratorRoleName);
                case UserRolesEnum.Principal:
                    return await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == PrincipalRoleName);
                case UserRolesEnum.Teacher:
                    return await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == TeacherRoleName);
                case UserRolesEnum.Parent:
                    return await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == ParentRoleName);
                case UserRolesEnum.Student:
                    return await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == StudentRoleName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }
    }
}
