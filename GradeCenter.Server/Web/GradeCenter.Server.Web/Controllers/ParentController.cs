namespace GradeCenter.Server.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class ParentController : ApiController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAbsencesService absencesService;
        private readonly IGradeService gradeService;

        public ParentController(
            IUserService userService,
            IAbsencesService absencesService,
            IGradeService gradeService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.absencesService = absencesService;
            this.gradeService = gradeService;
            this.userManager = userManager;
        }

        [Authorize(Roles = ParentRoleName)]
        [HttpGet]
        [Route("ChildPresences/{childId?}")]
        public async Task<ActionResult> ChildPresences(string childId = null)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!await this.userService.IsParentAsync(userId))
            {
                return this.BadRequest();
            }

            if (childId != null && !await this.userService.IsChildOfParentAsync(userId, childId))
            {
                return this.BadRequest();
            }

            var result = await this.absencesService.GetChildAbsencesStatisticsAsync(userId, childId);

            return this.Ok(result);
        }

        [Authorize(Roles = ParentRoleName)]
        [HttpGet]
        [Route("ChildGrades/{childId?}")]
        public async Task<ActionResult> ChildGrades(string childId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!await this.userService.IsParentAsync(userId))
            {
                return this.BadRequest();
            }

            if (childId != null && !await this.userService.IsChildOfParentAsync(userId, childId))
            {
                return this.BadRequest();
            }

            var result = await this.gradeService.GetChildGradeStatisticsAsync(userId, childId);

            return this.Ok(result);
        }
    }
}
