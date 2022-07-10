namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using GradeCenter.Server.Web.ViewModels.Curriculums;
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

        public async Task<T> GetByTermAndClassIdAsync<T>(int term, int classId)
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

        public async Task<T> GetByClassIdAsync<T>(int classId)
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

        public async Task<int> CreateAsync(int term, int classId, List<SubjectInputModel> subjects, List<TeacherInputModel> teachers)
        {
            var curriculum = new Curriculum
            {
                Term = term,
                ClassId = classId,
            };

            var curriculumsSubjects = subjects.Select(s => new CurriculumSubject
            {
                SubjectId = s.Id,
                Curriculum = curriculum,
            });

            var curriculumsTeachers = teachers.Select(t => new CurriculumTeacher()
            {
                TeacherId = t.Id,
                Curriculum = curriculum,
            });

            await this.dbContext.Curriculums.AddAsync(curriculum);
            await this.dbContext.CurriculumsSubjects.AddRangeAsync(curriculumsSubjects);
            await this.dbContext.CurriculumsTeachers.AddRangeAsync(curriculumsTeachers);

            await this.dbContext.SaveChangesAsync();

            return curriculum.Id;
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
            var deleteCurriculum = await this.dbContext.Curriculums.FirstOrDefaultAsync(s => s.Id == id);
            if (deleteCurriculum == null)
            {
                return false;
            }

            this.dbContext.Curriculums.Remove(deleteCurriculum);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
