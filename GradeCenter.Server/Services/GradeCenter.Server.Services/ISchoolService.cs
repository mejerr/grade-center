namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISchoolService
    {
        public Task<T> GetByIdAsync<T>(int id);

        public Task<T> GetByNameAsync<T>(string name);

        public Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<int> CreateAsync(string name, string address);

        public Task<bool> UpdateAsync(int id, string name, string address);

        public Task<bool> DeleteAsync(int id);

        public Task<bool> HasSchoolWithNameAsync(string name);
    }
}
