using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel;

namespace ArchiveManagement.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly UserManager<IdentityUser>   _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public TestController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _configuration=configuration;

        }
        [HttpGet]
        [Route("Test")]
        public IEnumerable<string> Tester()
        {

            return new List<string> { "You hit me","khalid","Dina"};
        }
    }
}
