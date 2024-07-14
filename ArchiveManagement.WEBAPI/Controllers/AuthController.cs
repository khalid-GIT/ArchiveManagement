using ArchiveManagement.WEBAPI.Models;

using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
//using ArchiveManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
//using ArchiveManagement.BLL.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.BLL.Dtos;
using System.Security.Claims;



//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;




//using System.Web.Http;




namespace ArchiveManagement.WEBAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

        private readonly IAuthServices _authServices;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //public SignInManager<IdentityUser> SignInManager;

        public AuthController(IAuthServices authServices, UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _authServices = authServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager ;

        }

        [HttpPost("Register")]

        [AllowAnonymous]
        public async Task<bool> Register(RegisterBindingModel userModel)
        {
            //CreatPassWordhash(request.Password, out byte[] passwordhash, out byte[] passwordsalt);
            if (!ModelState.IsValid)
            {
                return false;
            }
            IdentityUser user = new IdentityUser
            {
                Email = userModel.Email,
                UserName = userModel.UserName,
                // PasswordHash = userModel.Password
            };
            return await _authServices.RegisterUser(user, userModel.Password);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            //check if te user exist


            if (ModelState.IsValid)
            {
                //var userdto = new UserDto
                //{
                //    Email = user.Email,
                //    Username = user.UserName,
                //    Password = user.Password
                //};

                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    // PasswordHash = userModel.Password
                };

                var result = await _authServices.Login(user, model.Password);
                if (result == true)
                {
                    var stringToken = _authServices.GeneritTokeString(user, model.Password);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(stringToken);
                }
                return BadRequest();
            }
            return BadRequest("Bad data");
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            //    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //  var currentUserId = User.Claims.ToList().FirstOrDefault(x => x.Type == "").Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(currentUserId);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password changed succesfully");
            }
            else return Unauthorized("An error occurred while attempting to change password");

        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager != null)
            {
                IdentityUser user = new IdentityUser();
                await _signInManager.SignOutAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
