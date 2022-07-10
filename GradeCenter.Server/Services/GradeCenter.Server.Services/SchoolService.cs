namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SchoolService : ISchoolService
    {
        private readonly GradeCenterDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public SchoolService(
            GradeCenterDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.userService = userService;
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

        public async Task<string> SetPrincipalAsync(string userId, int schoolId)
        {
            var currentPrincipalId = await this.userService.GetSchoolPrincipalIdAsync(userId, schoolId);
            var currentPrincipal = await this.userManager.FindByIdAsync(currentPrincipalId);

            currentPrincipal.SchoolId = null;
            await this.userManager.UpdateAsync(currentPrincipal);

            var newPrincipal = await this.userManager.FindByIdAsync(userId);
            newPrincipal.SchoolId = schoolId;
            await this.userManager.UpdateAsync(newPrincipal);

            await this.dbContext.SaveChangesAsync();

            return newPrincipal.Id;
        }
    }
}
