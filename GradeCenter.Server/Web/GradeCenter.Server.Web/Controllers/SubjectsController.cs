namespace GradeCenter.Server.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Subjects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class SubjectsController : ApiController
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] CreateSubjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (await this.subjectService.HasSubjectWithNameAsync(model.Name))
            {
                return this.BadRequest();
            }

            var id = await this.subjectService.CreateAsync(model.Name);

            return this.Created(nameof(this.Create), id);
        }

        [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName}")]
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult<SubjectViewModel>> Details(int id)
        {
            var result = await this.subjectService.GetByIdAsync<SubjectViewModel>(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] EditSubjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.subjectService.UpdateAsync(id, model.Name);
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
            var isDeleted = await this.subjectService.DeleteAsync(id);
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
            return this.Ok(await this.subjectService.GetSubjectsAsync<SubjectViewModel>());
        }

        // [Authorize(Roles = $"{AdministratorRoleName},{PrincipalRoleName}")]
        [HttpGet]
        [Route("TeacherSubjects/{teacherId}")]
        public async Task<ActionResult> GetTeacherSubjects(string teacherId)
        {
            return this.Ok(await this.subjectService.GetSubjectsByTeacherIdAsync<SubjectViewModel>(teacherId));
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPut]
        [Route("EditTeacherSubjects/{id}")]
        public async Task<ActionResult> EditTeacherSubjects(string teacherId, [FromBody] EditTeacherSubjectsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var isUpdated = await this.subjectService.EditTeacherSubjectsAsync(teacherId, model);
            if (!isUpdated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
