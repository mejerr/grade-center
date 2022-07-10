namespace GradeCenter.Server.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using GradeCenter.Server.Services;
    using Microsoft.AspNetCore.Mvc;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class StudentsController : ApiController
    {
        private readonly IUserService userService;

        public StudentsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("/AddToClass{userId}/{classId}")]
        public async Task<ActionResult> AddToClass(string userId, int? classId)
        {
            if (userId == null)
            {
                return this.BadRequest();
            }

            if (classId == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.AddStudentToClassAsync(userId, classId.Value);

            return this.Ok(result);
        }

        [HttpPost]
        [Route("/RemoveFromClass{userId}")]
        public async Task<ActionResult> RemoveFromClass(string userId)
        {
            if (userId == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.RemoveStudentFromClassAsync(userId);

            return this.Ok(result);
        }
    }
}
