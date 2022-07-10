namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISchoolService
    {
        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string name, string address);

        Task<bool> UpdateAsync(int id, string name, string address);

        Task<bool> DeleteAsync(int id);

        Task<bool> HasSchoolWithNameAsync(string name);

        Task<string> SetPrincipalAsync(string userId, int schoolId);
    }
}
