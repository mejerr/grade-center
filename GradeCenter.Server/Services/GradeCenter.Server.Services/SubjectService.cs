using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeCenter.Server.Data;
using GradeCenter.Server.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GradeCenter.Server.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly GradeCenterDbContext dbContext;

        public SubjectService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetSubjects<T>()
        {
            return await this.dbContext.Subjects.To<T>().ToListAsync();
        }
    }
}
