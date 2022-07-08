using GradeCenter.Server.Data.Models;

namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICurriculumService
    {
        public Task<T> GetByIdAsync<T>(int id);

        public Task<T> GetByTermAsync<T>(int term);

        public Task<T> GetByClassIdAsync<T>(int classId);

        public Task<T> GetByTermAndClassIdAsync<T>(int term, int classId);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(int term, int classId, List<Subject> subjects, List<ApplicationUser> users);

        public Task<bool> UpdateAsync(int id, int term, int classId);

        public Task<bool> DeleteAsync(int id);
    }
}
