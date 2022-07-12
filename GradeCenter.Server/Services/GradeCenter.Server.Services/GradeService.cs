namespace GradeCenter.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Data.Models.Enums;
    using GradeCenter.Server.Services.Mapping;
    using GradeCenter.Server.Web.ViewModels.Grade;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

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

        public async Task<int> AddGradeAsync(decimal grade, string userId, int subjectId, GradeType gradeType, DateTime? dateOfGrade = null)
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

            return userGrade.Id;
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
            var userGrade = await this.dbContext
                .UsersGrades
                .Where(ug => ug.Id == id)
                .FirstOrDefaultAsync();

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
            var userGrade = await this.dbContext
                .UsersGrades
                .Where(ug => ug.Id == id)
                .FirstOrDefaultAsync();

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

        public async Task<GradeStatisticsViewModel> GetChildGradeStatisticsAsync(string parentId, string childId)
        {
            var gradeStatistics = new GradeStatisticsViewModel();

            var teachers = await this.dbContext.CurriculumsTeachers
                .Include(ct => ct.Teacher)
                .ToListAsync();

            var query = this.dbContext.UsersGrades
                .Where(ug => ug.User.UsersInferiorRelations
                    .Any(uir => uir.UserSuperiorId == parentId));

            if (childId != null)
            {
                query = query.Where(ug => ug.UserId == childId);
            }

            gradeStatistics.GradeStatistics = await query
                .Select(ug => new GradeStatistics()
                {
                    Grade = ug.Grade,
                    GradeType = ug.GradeType,
                    DateOfGrade = ug.DateOfGrade,
                    StudentName = ug.User.FullName,
                    SubjectName = ug.Subject.Name,
                    TeacherName = teachers
                        .FirstOrDefault(t => t.Curriculum.ClassId == ug.User.ClassId).Teacher.FullName,
                })
                .ToListAsync();

            return gradeStatistics;
        }

        public async Task<GradeStatisticsViewModel> GetGradeStatisticsAsync(int? schoolId = null, int? subjectId = null)
        {
            var gradeStatistics = new GradeStatisticsViewModel();

            if (schoolId == null && subjectId == null)
            {
                return gradeStatistics;
            }

            var teachers = await this.dbContext.CurriculumsTeachers
                .Include(ct => ct.Teacher)
                .ToListAsync();

            var query = Enumerable.Empty<UserGrade>().AsQueryable();

            if (schoolId != null)
            {
                query = query.Where(ug => ug.User.SchoolId == schoolId.Value);
            }

            if (subjectId != null)
            {
                query = query.Where(ug => ug.SubjectId == subjectId);
            }

            gradeStatistics.GradeStatistics = await query
                .Select(ug => new GradeStatistics()
                {
                    Grade = ug.Grade,
                    GradeType = ug.GradeType,
                    DateOfGrade = ug.DateOfGrade,
                    StudentName = ug.User.FullName,
                    SubjectName = ug.Subject.Name,
                    TeacherName = teachers
                        .FirstOrDefault(t => t.Curriculum.ClassId == ug.User.ClassId).Teacher.FullName,
                })
                .ToListAsync();

            return gradeStatistics;
        }

        public async Task<GradeStatisticsViewModel> GetGradeStatisticsByTeacherAsync(string teacherId, int? schoolId = null)
        {
            var gradeStatistics = new GradeStatisticsViewModel();

            var teacherQuery = this.dbContext.Users
                .Where(t => t.Id == teacherId);

            if (schoolId != null)
            {
                teacherQuery = teacherQuery.Where(t => t.SchoolId == schoolId.Value);
            }

            var teacher = await teacherQuery.FirstOrDefaultAsync();

            var gradeQuery = this.dbContext.UsersGrades
                .Where(ug => teacher.UsersSubjects
                    .Any(us => us.Subject == ug.Subject
                               && teacher.CurriculumsTeachers
                                   .Any(ct => ct.Curriculum.ClassId == ug.User.ClassId)));
            if (schoolId != null)
            {
                gradeQuery = gradeQuery.Where(ug => ug.User.SchoolId == schoolId.Value);
            }

            gradeStatistics.GradeStatistics = await gradeQuery
                .Select(ug => new GradeStatistics()
                {
                    Grade = ug.Grade,
                    GradeType = ug.GradeType,
                    DateOfGrade = ug.DateOfGrade,
                    StudentName = ug.User.FullName,
                    SubjectName = ug.Subject.Name,
                    TeacherName = teacher.FullName,
                })
                .ToListAsync();

            return gradeStatistics;
        }
    }
}
