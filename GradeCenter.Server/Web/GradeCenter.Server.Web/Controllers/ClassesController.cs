namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Class;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class ClassesController : ApiController
    {
        private readonly IClassService classService;

        public ClassesController(IClassService classService)
        {
            this.classService = classService;
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("/Create")]
        public async Task<ActionResult> Create([FromBody] CreateClassInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = await this.classService.CreateAsync(model.Number, model.Division, model.SchoolId);

            return this.Created(nameof(this.Create), id);
        }

        [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName}")]
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult<ClassViewModel>> Details(int id)
        {
            var result = await this.classService.GetByIdAsync<ClassViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateClassInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.classService.UpdateAsync(id, model.Number, model.Division);
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
            var isDeleted = await this.classService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> All()
        {
            return this.Ok(await this.classService.GetAllAsync<ClassViewModel>());
        }
    }
}
