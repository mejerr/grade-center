namespace GradeCenter.Server.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Data.Models.Enums;
    using GradeCenter.Server.Services.Mapping;
    using GradeCenter.Server.Web.ViewModels.Absences;
    using Microsoft.EntityFrameworkCore;

    public class AbsencesService : IAbsencesService
    {
        private readonly GradeCenterDbContext dbContext;

        public AbsencesService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetUserPresenceAsync<T>(int id)
        {
            return await this.dbContext
                .UsersPresences
                .Where(ug => ug.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddAbsenceAsync(string userId, int subjectId, PresenceType presenceType, DateTime? dateOfClass = null)
        {
            var userPresence = new UserPresence()
            {
                SubjectId = subjectId,
                UserId = userId,
                DateOfClass = dateOfClass ?? DateTime.UtcNow,
                PresenceType = presenceType,
            };

            await this.dbContext.UsersPresences.AddAsync(userPresence);
            await this.dbContext.SaveChangesAsync();

            return userPresence.Id;
        }

        public async Task<bool> EditAbsenceAsync(int userPresenceId, PresenceType? presenceType, DateTime? dateOfClass = null)
        {
            var userPresence = await this.GetUserPresenceAsync<UserPresence>(userPresenceId);

            if (userPresence == null)
            {
                return false;
            }

            userPresence.PresenceType = presenceType ?? userPresence.PresenceType;
            userPresence.DateOfClass = dateOfClass ?? userPresence.DateOfClass;

            this.dbContext.UsersPresences.Update(userPresence);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAbsenceAsync(int id)
        {
            var userPresence = await this.GetUserPresenceAsync<UserPresence>(id);

            if (userPresence == null)
            {
                return false;
            }

            this.dbContext.UsersPresences.Remove(userPresence);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<AbsencesStatisticsViewModel> GetAllAbsenceStatisticsAsync(int? schoolId = null, int? subjectId = null)
        {
            var absencesStatistics = new AbsencesStatisticsViewModel();

            if (schoolId == null && subjectId == null)
            {
                return absencesStatistics;
            }

            var query = Enumerable.Empty<UserPresence>().AsQueryable();
            if (schoolId != null)
            {
                query = this.dbContext.UsersPresences.Where(up => up.User.SchoolId == schoolId.Value);
            }

            if (subjectId != null)
            {
                query = query.Where(up => up.SubjectId == subjectId.Value);
            }

            absencesStatistics.AbsencesStatistics = await query
                .Select(q => new AbsencesStatistics()
                {
                    StudentName = q.User.FullName,
                    SubjectName = q.Subject.Name,
                    DateOfClass = q.DateOfClass,
                    PresenceType = q.PresenceType,
                })
                .ToListAsync();

            return absencesStatistics;
        }

        public async Task<AbsencesStatisticsViewModel> GetAllAbsenceStatisticsByTeacherAsync(string teacherId, int? schoolId = null)
        {
            var absencesStatistics = new AbsencesStatisticsViewModel();

            var teacherQuery = this.dbContext.Users
                .Where(t => t.Id == teacherId);

            if (schoolId != null)
            {
                teacherQuery = teacherQuery.Where(t => t.SchoolId == schoolId.Value);
            }

            var teacher = await teacherQuery.FirstOrDefaultAsync();

            var presenceQuery = this.dbContext.UsersPresences
                .Where(up => teacher.UsersSubjects
                    .Any(us => us.Subject == up.Subject
                               && teacher.CurriculumsTeachers
                                   .Any(ct => ct.Curriculum.ClassId == up.User.ClassId)));
            if (schoolId != null)
            {
                presenceQuery = presenceQuery.Where(ug => ug.User.SchoolId == schoolId.Value);
            }

            absencesStatistics.AbsencesStatistics = await presenceQuery
                .Select(q => new AbsencesStatistics()
                {
                    StudentName = q.User.FullName,
                    SubjectName = q.Subject.Name,
                    DateOfClass = q.DateOfClass,
                    PresenceType = q.PresenceType,
                    TeacherName = teacher.FullName,
                })
                .ToListAsync();

            return absencesStatistics;
        }

        public async Task<AbsencesStatisticsViewModel> GetChildAbsencesStatisticsAsync(string parentId, string childId)
        {
            var absencesStatistics = new AbsencesStatisticsViewModel();

            var query = this.dbContext.UsersPresences
                .Where(up => up.User.UsersInferiorRelations
                    .Any(uir => uir.UserSuperiorId == parentId));

            if (childId != null)
            {
                query = query.Where(up => up.UserId == childId);
            }

            absencesStatistics.AbsencesStatistics = await query
                .Select(q => new AbsencesStatistics()
                {
                    StudentName = q.User.FullName,
                    SubjectName = q.Subject.Name,
                    DateOfClass = q.DateOfClass,
                    PresenceType = q.PresenceType,
                })
                .ToListAsync();

            return absencesStatistics;
        }
    }
}
