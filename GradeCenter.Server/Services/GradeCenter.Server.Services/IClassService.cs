namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClassService
    {
        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNumberAndDivisionAsync<T>(int number, string devision, int schoolId);

        Task<T> GetByNumberAsync<T>(int number, int schoolId);

        Task<T> GetBySchoolIdAsync<T>(int schoolId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(int number, string devision, int schoolId);

        Task<bool> UpdateAsync(int id, int number, string devision);

        Task<bool> DeleteAsync(int id);
    }
}
