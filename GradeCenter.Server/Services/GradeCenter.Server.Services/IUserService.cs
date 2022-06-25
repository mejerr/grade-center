using System.Collections.Generic;
using GradeCenter.Server.Common.Enums;
using GradeCenter.Server.Data.Models;

namespace GradeCenter.Server.Services
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> SetRoleAsync(string userId, string roleId);

        Task<bool> ClearRolesAsync(string userId);

        Task<string> AddStudentToClassAsync(string userId, int classId);

        Task<string> RemoveStudentFromClassAsync(string userId);

        Task<string> GetSchoolPrincipalIdAsync(string userId, int schoolId);

        Task<bool> IsTeacherAsync(string userId);

        Task<bool> IsStudentAsync(string userId);

        Task<bool> IsIsParentAsync(string userId);

        Task<bool> RemoveUserSubjectAsync(string userId, int subjectId);

        Task<bool> RemoveParentRelationsAsync(string userId);

        Task<IEnumerable<ApplicationUser>> GetTeachersAsync(int? schoolId = null);

        Task<IEnumerable<ApplicationUser>> GetStudentsAsync(int? schoolId = null);

        Task<IEnumerable<ApplicationUser>> GetParentsAsync(int? schoolId = null);

        Task<T> GetByNameAsync<T>(string fullName, UserRolesEnum role);

        Task<T> GetByIdAsync<T>(string id, UserRolesEnum role);

        Task<IEnumerable<T>> GetUserDependendsAsync<T>(string id, UserRolesEnum role);
    }
}
