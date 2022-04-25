namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SchoolService : ISchoolService
    {
        private readonly GradeCenterDbContext dbContext;

        public SchoolService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.dbContext.Schools
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByNameAsync<T>(string name)
        {
            return await this.dbContext.Schools
                .Where(s => s.Name == name)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.dbContext.Schools
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CreateAsync(string name, string address)
        {
            var school = new School
            {
                Name = name,
                Address = address,
            };

            await this.dbContext.Schools.AddAsync(school);
            await this.dbContext.SaveChangesAsync();

            return school.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name, string address)
        {
            var school = await this.dbContext.Schools.FirstOrDefaultAsync(s => s.Id == id);
            if (school == null)
            {
                return false;
            }

            school.Name = name;
            school.Address = address;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var school = await this.dbContext.Schools.FirstOrDefaultAsync(s => s.Id == id);
            if (school == null)
            {
                return false;
            }

            this.dbContext.Schools.Remove(school);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> HasSchoolWithNameAsync(string name)
        {
            return await this.dbContext.Schools
                .AnyAsync(s => s.Name.ToLower() == name.ToLower());
        }
    }
}
