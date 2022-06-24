using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeCenter.Server.Data;
using GradeCenter.Server.Data.Models;
using GradeCenter.Server.Data.Models.Enums;
using GradeCenter.Server.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

namespace GradeCenter.Server.Services
{
    public class GradeService : IGradeService
    {
        private readonly GradeCenterDbContext dbContext;
        private readonly RoleManager<ApplicationRole> roleManager;

        public GradeService(
            GradeCenterDbContext dbContext,
            RoleManager<ApplicationRole> roleManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }

        public async Task<bool> AddGradeAsync(decimal grade, string userId, int subjectId, GradeType gradeType, DateTime? dateOfGrade)
        {
            var userGrade = new UserGrade()
            {
                SubjectId = subjectId,
                Grade = grade,
                UserId = userId,
                DateOfGrade = dateOfGrade ?? DateTime.UtcNow,
                GradeType = gradeType,
            };

            await this.dbContext.UsersGrades.AddAsync(userGrade);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetUserGradeAsync<T>(int id)
        {
            return await this.dbContext
                .UsersGrades
                .Where(ug => ug.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetUserGradesAsync<T>(int id)
        {
            return await this.dbContext
                .UsersGrades
                .Where(ug => ug.Id == id)
                .To<T>()
                .ToListAsync();
        }

        public async Task<bool> RemoveGradeAsync(int id)
        {
            var userGrade = await this.GetUserGradeAsync<UserGrade>(id);

            if (userGrade == null)
            {
                return false;
            }

            this.dbContext.UsersGrades.Remove(userGrade);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditGradeAsync(int id, decimal grade, GradeType? gradeType = null, DateTime? dateOfGrade = null)
        {
            var userGrade = await this.GetUserGradeAsync<UserGrade>(id);

            if (userGrade == null)
            {
                return false;
            }

            userGrade.Grade = grade;
            userGrade.GradeType = gradeType ?? userGrade.GradeType;
            userGrade.DateOfGrade = dateOfGrade ?? userGrade.DateOfGrade;

            this.dbContext.UsersGrades.Update(userGrade);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetChildGradesAsync<T>(string parentId, string childId = null)
        {
            var parentRole = await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Name == ParentRoleName);

            var children = childId != null
                ? await this.dbContext.Users
                    .Where(u =>
                        u.UsersInferiorRelations
                            .Any(uir => uir.UserInferiorId == childId
                                        && uir.UserRole == parentRole
                                        && uir.UserSuperiorId == parentId))
                    .ToListAsync()
                : await this.dbContext.Users
                    .Where(u =>
                        u.UsersInferiorRelations
                            .Any(uir => uir.UserSuperiorId == parentId))
                    .ToListAsync();

            var userGrades = await this.dbContext.UsersGrades
                .Where(ug => children.Contains(ug.User))
                .To<T>()
                .ToListAsync();

            return userGrades;
        }
    }
}
