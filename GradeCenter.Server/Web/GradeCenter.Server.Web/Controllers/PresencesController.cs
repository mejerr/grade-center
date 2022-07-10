namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Absences;
    using GradeCenter.Server.Web.ViewModels.Grade;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class PresencesController : ApiController
    {
        private readonly IAbsencesService absencesService;

        public PresencesController(IAbsencesService absencesService)
        {
            this.absencesService = absencesService;
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpPost]
        [Route("SetPresence")]
        public async Task<ActionResult> SetPresence([FromBody] PresenceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = await this.absencesService
                .AddAbsenceAsync(model.UserId, model.SubjectId, model.PresenceType, model.DateOfClass);

            return this.Created(nameof(this.SetPresence), id);
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpDelete]
        [Route("RemovePresence/{userPresenceId}")]
        public async Task<ActionResult> RemovePresence(int userPresenceId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.absencesService.RemoveAbsenceAsync(userPresenceId);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpPut]
        [Route("EditPresence")]
        public async Task<ActionResult> EditPresence([FromBody] EditPresenceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.absencesService.EditAbsenceAsync(model.Id, model.PresenceType, model.DateOfClass);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}