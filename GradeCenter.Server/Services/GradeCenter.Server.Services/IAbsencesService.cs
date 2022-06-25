namespace GradeCenter.Server.Services
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Web.ViewModels.Absences;

    public interface IAbsencesService
    {
        Task<AbsencesStatisticsViewModel> GetAllAbsenceStatistics(int? schoolId = null, int? subjectId = null);
    }
}
