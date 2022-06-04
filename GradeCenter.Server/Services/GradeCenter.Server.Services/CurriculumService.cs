namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CurriculumService : ICurriculumService
    {
        private readonly GradeCenterDbContext dbContext;

        public CurriculumService(GradeCenterDbContext dbContext)
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

        public async Task<T> GetByTermAndClassId<T>(int term, int classId)
        {
            return await this.dbContext.Curriculums
                .Where(s => s.Term == term &&
                       s.ClassId == classId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByTermAsync<T>(int term)
        {
            return await this.dbContext.Curriculums
                .Where(s => s.Term == term)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByClassId<T>(int classId)
        {
            return await this.dbContext.Curriculums
                .Where(s => s.ClassId == classId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.dbContext.Curriculums
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CreateAsync(int term, int classId)
        {
            var addCurriculum = new Curriculum
            {
                Term = term,
                ClassId = classId,
            };

            await this.dbContext.Curriculums.AddAsync(addCurriculum);
            await this.dbContext.SaveChangesAsync();

            return addCurriculum.Id;
        }

        public async Task<bool> UpdateAsync(int id, int term, int classId)
        {
            var updateCurriculum = await this.dbContext.Curriculums.FirstOrDefaultAsync(s => s.Id == id);
            if (updateCurriculum == null)
            {
                return false;
            }

            updateCurriculum.Term = term;
            updateCurriculum.ClassId = classId;

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
