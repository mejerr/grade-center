namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Web.ViewModels.Curriculums;

    public interface ICurriculumService
    {
        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByTermAsync<T>(int term);

        Task<T> GetByClassIdAsync<T>(int classId);

        Task<T> GetByTermAndClassIdAsync<T>(int term, int classId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(int term, int classId, List<SubjectInputModel> subjects, List<TeacherInputModel> teachers);

        Task<bool> UpdateAsync(int id, int term, int classId);

        Task<bool> DeleteAsync(int id);
    }
}
