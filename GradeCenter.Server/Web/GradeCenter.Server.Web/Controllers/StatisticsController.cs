namespace GradeCenter.Server.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Absences;
    using GradeCenter.Server.Web.ViewModels.Grade;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class StatisticsController : ApiController
    {
        private readonly IUserService userService;
        private readonly IGradeService gradeService;
        private readonly IAbsencesService absencesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public StatisticsController(
            IUserService userService,
            IGradeService gradeService,
            IAbsencesService absencesService,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userService = userService;
            this.gradeService = gradeService;
            this.absencesService = absencesService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // [Authorize(Roles = $"{PrincipalRoleName},{AdministratorRoleName}")]
        [HttpGet]
        [Route("GradeStatistics/schoolId/{schoolId}/teacherId/{teacherId}/subjectId/{subjectId}")]
        public async Task<ActionResult> GetGradeStatistics(
            int? schoolId,
            string teacherId,
            int? subjectId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(PrincipalRoleName) && schoolId != null)
            {
                return this.Unauthorized();
            }

            var result = new GradeStatisticsViewModel();

            if (teacherId != null)
            {
                result = await this.gradeService.GetGradeStatisticsByTeacherAsync(teacherId, schoolId);
            }
            else
            {
                result = await this.gradeService.GetGradeStatisticsAsync(schoolId, subjectId);
            }

            return this.Ok(result);
        }

        // [Authorize(Roles = $"{PrincipalRoleName},{AdministratorRoleName}")]
        [HttpGet]
        [Route("AbsencesStatistics/schoolId/{schoolId}/teacherId/{teacherId}/subjectId/{subjectId}")]
        public async Task<ActionResult> GetAbsencesStatistics(
            int? schoolId,
            string teacherId,
            int? subjectId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(PrincipalRoleName) && schoolId != null)
            {
                return this.Unauthorized();
            }

            var result = new AbsencesStatisticsViewModel();

            if (teacherId != null)
            {
                result = await this.absencesService.GetAllAbsenceStatisticsByTeacherAsync(teacherId, schoolId);
            }
            else
            {
                result = await this.absencesService.GetAllAbsenceStatisticsAsync(schoolId, subjectId);
            }

            return this.Ok(result);
        }
    }
}
