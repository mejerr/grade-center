namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClassService
    {
        public Task<T> GetByIdAsync<T>(int id);

        public Task<T> GetByNumberAndDivisionAsync<T>(int number, string devision, int schoolId);

        public Task<T> GetByNumber<T>(int number, int schoolId);

        public Task<T> GetBySchoolId<T>(int schoolId);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<int> CreateAsync(int number, string devision, int schoolId);

        public Task<bool> UpdateAsync(int id, int number, string devision);

        public Task<bool> DeleteAsync(int id);
    }
}
