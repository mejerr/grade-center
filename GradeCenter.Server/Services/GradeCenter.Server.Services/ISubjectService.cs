namespace GradeCenter.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GradeCenter.Server.Web.ViewModels.Subjects;

    public interface ISubjectService
    {
        Task<int> CreateAsync(string name);

        Task<bool> UpdateAsync(int id, string name);

        Task<bool> DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> HasSubjectWithNameAsync(string name);

        Task<IEnumerable<T>> GetSubjectsAsync<T>();

        Task<IEnumerable<T>> GetSubjectsByTeacherIdAsync<T>(string teacherId);

        Task<bool> EditTeacherSubjectsAsync(string teacherId, EditTeacherSubjectsInputModel subjects);
    }
}
