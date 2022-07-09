namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Curriculums;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class CurriculumsController : ApiController
    {
        private readonly ICurriculumService curriculumService;

        public CurriculumsController(ICurriculumService curriculumService)
        {
            this.curriculumService = curriculumService;
        }

        [Authorize(Roles = "Administrator,Director")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCurriculumInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = -1;

            // var id = await this.curriculumService.CreateAsync(model.Term, model.ClassId);
            return this.Created(nameof(this.Create), id);
        }

        [HttpGet]
        [Route("curriculum/details/{id}")]
        public async Task<ActionResult<CurriculumViewModel>> Details(int id)
        {
            var result = await this.curriculumService.GetByIdAsync<CurriculumViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize(Roles = "Administrator,Director")]
        [HttpPut]
        [Route("curriculum/update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCurriculumInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.curriculumService.UpdateAsync(id, model.Term, model.ClassId);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [Authorize(Roles = "Administrator,Director")]
        [HttpDelete]
        [Route("curriculum/delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            var isDeleted = await this.curriculumService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            return this.Ok(await this.curriculumService.GetAllAsync<CurriculumViewModel>());
        }
    }
}
