namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ClassService : IClassService
    {
        private readonly GradeCenterDbContext dbContext;

        public ClassService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.dbContext.Classes
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByNumberAndDivisionAsync<T>(int number, string division, int schoolId)
        {
            return await this.dbContext.Classes
                .Where(s => s.Number == number &&
                       s.Division == division &&
                       s.SchoolId == schoolId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByNumber<T>(int number, int schoolId)
        {
            return await this.dbContext.Classes
                .Where(s => s.Number == number &&
                       s.SchoolId == schoolId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetBySchoolId<T>(int schoolId)
        {
            return await this.dbContext.Classes
                .Where(s => s.SchoolId == schoolId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.dbContext.Classes
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CreateAsync(int number, string division, int schoolId)
        {
            var addClass = new Class
            {
                Number = number,
                Division = division,
                SchoolId = schoolId,
            };

            await this.dbContext.Classes.AddAsync(addClass);
            await this.dbContext.SaveChangesAsync();

            return addClass.Id;
        }

        public async Task<bool> UpdateAsync(int id, int number, string division)
        {
            var updateClass = await this.dbContext.Classes.FirstOrDefaultAsync(s => s.Id == id);
            if (updateClass == null)
            {
                return false;
            }

            updateClass.Number = number;
            updateClass.Division = division;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleteClass = await this.dbContext.Classes.FirstOrDefaultAsync(s => s.Id == id);
            if (deleteClass == null)
            {
                return false;
            }

            this.dbContext.Classes.Remove(deleteClass);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
