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
using MySqlX.XDevAPI;



//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;




//using System.Web.Http;




namespace ArchiveManagement.WEBAPI.Controllers
{
    [Authorize]
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
            , SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _authServices = authServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        [Authorize(Roles = "Admin")]
        // [AllowAnonymous]
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
            bool result = await _authServices.RegisterUser(user, userModel.Password);

            if (result)
            {
                if (await _roleManager.RoleExistsAsync(userModel.Role))
                {

                    await _userManager.AddToRoleAsync(user, userModel.Role);

                }
            }
            return result;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //check if te user exist
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    //UserName = model.UserName,
                    // PasswordHash = userModel.Password
                };
                var result = await _authServices.Login(user, model.Password);
               if (result == true)
                {
                    IdentityUser _user = new IdentityUser();
                    _user.Email = model.Email;
                    
                    var _identityUser = await _userManager.FindByEmailAsync(_user.Email);
                    if (_identityUser != null)
                    {
                        _user.UserName = _identityUser.UserName;
                        _user.Id = _identityUser.Id;
                    }
                    var stringToken =  _authServices.GeneritTokeString(_user, model.Password);
                    //Save a value og Token
                    //_session(/*stringToken*/, "Token");
                    var results = await _signInManager.PasswordSignInAsync(_user.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (!results.Succeeded)
                    {
                        return BadRequest("Invalid login attempt");
                    }

                    // Si l'authentification a réussi, alors appeler SignInAsync
                    var users = await _userManager.FindByEmailAsync(_user.Email);
                    if (users != null)
                    {
                        await _signInManager.SignInAsync(_user, isPersistent: false);
                    }
                    //await _signInManager.SignInAsync(_user, isPersistent: false);
                    var access_token = stringToken;
                    return Ok(new { token=access_token });
                }
                return BadRequest("User not exist");
            }
            return BadRequest("Bad data");
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
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
                return Ok("End Session");
            }
            return BadRequest();
        }
        //a faire demain

        [HttpPost("RemoveLogin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveLogin(RemoveLoginModel model)
        {
            IdentityUser userExist = new IdentityUser();
            userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                var result = await _userManager.DeleteAsync(userExist);
                if (result.Succeeded)
                {
                    return Ok("Done");
                }
            }
            return BadRequest("User not find");
        }
    }
}
