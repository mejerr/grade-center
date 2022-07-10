namespace GradeCenter.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using GradeCenter.Server.Web.ViewModels.Subjects;
    using Microsoft.EntityFrameworkCore;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class SubjectService : ISubjectService
    {
        private readonly GradeCenterDbContext dbContext;
        private readonly IUserService userService;

        public SubjectService(
            GradeCenterDbContext dbContext,
            IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public async Task<IEnumerable<T>> GetSubjectsAsync<T>()
        {
            return await this.dbContext.Subjects.To<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.dbContext.Subjects
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.dbContext.Subjects
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CreateAsync(string name)
        {
            var subject = new Subject
            {
                Name = name,
            };

            await this.dbContext.Subjects.AddAsync(subject);
            await this.dbContext.SaveChangesAsync();

            return subject.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var subject = await this.dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            subject.Name = name;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await this.dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            this.dbContext.Subjects.Remove(subject);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> HasSubjectWithNameAsync(string name)
        {
            return await this.dbContext.Subjects
                .AnyAsync(s => s.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<T>> GetSubjectsByTeacherIdAsync<T>(string teacherId)
        {
            if (!await this.userService.IsTeacherAsync(teacherId))
            {
                return null;
            }

            return await this.dbContext.UsersSubjects.Where(us => us.UserId == teacherId).To<T>().ToListAsync();
        }

        public async Task<bool> EditTeacherSubjectsAsync(string teacherId, EditTeacherSubjectsInputModel teacherSubjects)
        {
            var teacher = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == teacherId);
            var newSubjects = teacherSubjects
                .subjects
                .Select(s => new UserSubject
                {
                    SubjectId = s.Id,
                    UserId = teacherId,
                }).ToList();

            teacher.UsersSubjects = newSubjects;

            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
