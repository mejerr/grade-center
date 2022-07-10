namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Grade;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class GradesController : ApiController
    {
        private readonly IGradeService gradeService;

        public GradesController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpPost]
        [Route("SetGrade")]
        public async Task<ActionResult> SetGrade([FromBody] GradeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = await this.gradeService
                .AddGradeAsync(model.Grade, model.UserId, model.SubjectId, model.GradeType, model.DateOfGrade);

            return this.Created(nameof(this.SetGrade), id);
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpDelete]
        [Route("RemoveGrade/{userGradeId}")]
        public async Task<ActionResult> RemoveUserGrade(int userGradeId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.gradeService.RemoveGradeAsync(userGradeId);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName},{TeacherRoleName}")]
        [HttpPut]
        [Route("EditGrade")]
        public async Task<ActionResult> EditPresence([FromBody] EditGradeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.gradeService.EditGradeAsync(model.Id, model.Grade, model.GradeType, model.DateOfGrade);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
