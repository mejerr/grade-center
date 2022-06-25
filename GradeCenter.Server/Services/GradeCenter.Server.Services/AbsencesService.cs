namespace GradeCenter.Server.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Web.ViewModels.Absences;
    using Microsoft.EntityFrameworkCore;

    public class AbsencesService : IAbsencesService
    {
        private readonly GradeCenterDbContext dbContext;

        public AbsencesService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AbsencesStatisticsViewModel> GetAllAbsenceStatistics(int? schoolId = null, int? subjectId = null)
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
    }
}
