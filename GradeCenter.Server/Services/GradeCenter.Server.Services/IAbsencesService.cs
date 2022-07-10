namespace GradeCenter.Server.Services
{
    using System;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models.Enums;
    using GradeCenter.Server.Web.ViewModels.Absences;

    public interface IAbsencesService
    {
        Task<AbsencesStatisticsViewModel> GetAllAbsenceStatisticsAsync(int? schoolId = null, int? subjectId = null);

        Task<AbsencesStatisticsViewModel> GetAllAbsenceStatisticsByTeacherAsync(string teacherId, int? schoolId = null);

        Task<AbsencesStatisticsViewModel> GetChildAbsencesStatisticsAsync(string parentId, string childId);

        Task<int> AddAbsenceAsync(string userId, int subjectId, PresenceType presenceType, DateTime? dateOfClass = null);

        Task<bool> EditAbsenceAsync(int userPresenceId, PresenceType? presenceType, DateTime? dateOfClass = null);

        Task<bool> RemoveAbsenceAsync(int id);

        Task<T> GetUserPresenceAsync<T>(int id);
    }
}
