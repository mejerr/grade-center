namespace GradeCenter.Server.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        // [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}
