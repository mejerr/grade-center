namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.School;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class SchoolsController : ApiController
    {
        private readonly ISchoolService schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSchoolInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (await this.schoolService.HasSchoolWithNameAsync(model.Name))
            {
                return this.BadRequest();
            }

            var id = await this.schoolService.CreateAsync(model.Name, model.Address);

            return this.Created(nameof(this.Create), id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SchoolViewModel>> Details(int id)
        {
            var result = await this.schoolService.GetByIdAsync<SchoolViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSchoolInputModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.schoolService.UpdateAsync(id, model.Name, model.Address);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            var isDeleted = await this.schoolService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            return this.Ok(await this.schoolService.GetAllAsync<SchoolViewModel>());
        }
    }
}
