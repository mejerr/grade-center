namespace GradeCenter.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models.Enums;
    using GradeCenter.Server.Web.ViewModels.Grade;

    public interface IGradeService
    {
        Task<T> GetUserGradeAsync<T>(int id);

        Task<IEnumerable<T>> GetUserGradesAsync<T>(int id);

        Task<bool> AddGradeAsync(decimal grade, string userId, int subjectId, GradeType gradeType, DateTime? dateOfGrade);

        Task<bool> RemoveGradeAsync(int id);

        Task<bool> EditGradeAsync(int id, decimal grade, GradeType? gradeType = null, DateTime? dateOfGrade = null);

        Task<IEnumerable<T>> GetChildGradesAsync<T>(string parentId, string childId = null);

        Task<GradeStatisticsViewModel> GetGradeStatisticsAsync(int? schoolId = null, int? subjectId = null);

        Task<GradeStatisticsViewModel> GetGradeStatisticsByTeacherAsync(string teacherId, int? schoolId = null);
    }
}
