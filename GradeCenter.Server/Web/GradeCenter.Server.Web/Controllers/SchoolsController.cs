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

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] CreateSchoolInputModel model)
        {
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

        [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName}")]
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult<SchoolViewModel>> Details(int id)
        {
            var result = await this.schoolService.GetByIdAsync<SchoolViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSchoolInputModel model)
        {
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

        [Authorize(Roles = AdministratorRoleName)]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await this.schoolService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName}")]
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> All()
        {
            return this.Ok(await this.schoolService.GetAllAsync<SchoolViewModel>());
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("SetPrincipal/userId/{userId}/schoolId/{schoolId}")]
        public async Task<ActionResult> SetPrincipal(string userId, int schoolId)
        {
            if (userId == null)
            {
                return this.BadRequest();
            }

            var result = await this.schoolService.SetPrincipalAsync(userId, schoolId);

            return this.Ok(result);
        }
    }
}
