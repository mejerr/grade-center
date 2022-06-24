using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeCenter.Server.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<T>> GetSubjects<T>();
    }
}
