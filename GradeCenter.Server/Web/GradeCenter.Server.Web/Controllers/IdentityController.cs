namespace GradeCenter.Server.Web.Controllers
{
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services;
    using GradeCenter.Server.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Roles;

    public class IdentityController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IIdentityService identityService;
        private readonly IUserService userService;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<AppSettings> appSettings,
            IIdentityService identityService,
            IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityService = identityService;
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        // [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("/Register")]
        public async Task<ActionResult> Register(RegisterInputModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth,
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult<object>> Login(LoginInputModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return this.BadRequest();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return this.Unauthorized();
            }

            var encryptedToken = this.identityService.GenerateToken(this.appSettings.Secret, user.Id, user.UserName);

            return new
            {
                Token = encryptedToken,
            };
        }

        // [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("/SetRole/user/{userId}/role/{roleId}")]
        public async Task<ActionResult<object>> SetRole(string userId, string roleId)
        {
            if (userId == null)
            {
                return this.BadRequest();
            }

            if (roleId == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.SetRoleAsync(userId, roleId);

            if (result == null)
            {
                return this.BadRequest();
            }

            // userId
            return result;
        }

        // [Authorize(Roles = AdministratorRoleName)]
        [HttpPut]
        [Route("/Edit")]
        public async Task<ActionResult<object>> Edit(EditUserDataInputModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return this.BadRequest();
            }

            user.UserName = model.UserName ?? user.UserName;
            user.Address = model.Address ?? user.Address;
            user.Email = model.Email ?? user.Email;
            user.FullName = model.FullName ?? user.FullName;
            user.DateOfBirth = model.DateOfBirth ?? user.DateOfBirth;

            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        // [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("/RemoveDependent/superior/{userSuperiorId}/inferior/{userInferiorId}/role/{superiorRole}")]
        public async Task<ActionResult<object>> RemoveDependent(string userSuperiorId, string userInferiorId, string superiorRole)
        {
            if (userSuperiorId == null)
            {
                return this.BadRequest();
            }

            if (userInferiorId == null)
            {
                return this.BadRequest();
            }

            if (superiorRole == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.RemoveDependentsAsync(userSuperiorId, userInferiorId, superiorRole);

            if (result is false)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        // [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        [Route("/AddDependent/superior/{userSuperiorId}/inferior/{userInferiorId}/role/{superiorRole}")]
        public async Task<ActionResult<object>> AddDependent(string userSuperiorId, string userInferiorId, string superiorRole)
        {
            if (userSuperiorId == null)
            {
                return this.BadRequest();
            }

            if (userInferiorId == null)
            {
                return this.BadRequest();
            }

            if (superiorRole == null)
            {
                return this.BadRequest();
            }

            var role = await this.roleManager.FindByNameAsync(superiorRole);
            if (role == null)
            {
                return this.BadRequest();
            }

            var result = await this.userService.AddDependentAsync(userSuperiorId, userInferiorId, role.Id);

            return this.Ok();
        }
    }
}
