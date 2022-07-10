namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GradeCenter.Server.Common.Enums;
    using GradeCenter.Server.Data.Models;

    public interface IUserService
    {
        Task<string> SetRoleAsync(string userId, string roleId);

        Task<bool> ClearRolesAsync(string userId);

        Task<string> AddStudentToClassAsync(string userId, int classId);

        Task<string> RemoveStudentFromClassAsync(string userId);

        Task<string> GetSchoolPrincipalIdAsync(string userId, int schoolId);

        Task<bool> IsTeacherAsync(string userId);

        Task<bool> IsStudentAsync(string userId);

        Task<bool> IsParentAsync(string userId);

        Task<bool> IsChildOfParentAsync(string parentId, string childId);

        Task<bool> RemoveUserSubjectAsync(string userId, int subjectId);

        Task<bool> RemoveParentRelationsAsync(string userId);

        Task<bool> RemoveDependentsAsync(string userSuperiorId, string userInferiorId, string role);

        Task<bool> AddDependentAsync(string userSuperiorId, string userInferiorId, string roleId);

        Task<IEnumerable<ApplicationUser>> GetTeachersAsync(int? schoolId = null);

        Task<IEnumerable<ApplicationUser>> GetStudentsAsync(int? schoolId = null);

        Task<IEnumerable<ApplicationUser>> GetParentsAsync(int? schoolId = null);

        Task<T> GetByNameAsync<T>(string fullName, UserRolesEnum role);

        Task<T> GetByIdAsync<T>(string id, UserRolesEnum role);

        Task<IEnumerable<T>> GetUserDependendsAsync<T>(string id, UserRolesEnum role);
    }
}
