﻿using ArchiveManagement.WEBAPI.Models;

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

        public AuthController(IAuthServices authServices, UserManager<IdentityUser> userManager)
        {
            _authServices = authServices;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        public async Task<bool> Register(RegisterBindingModel user)
        {
            //CreatPassWordhash(request.Password, out byte[] passwordhash, out byte[] passwordsalt);
            if (!ModelState.IsValid)
            {
                return false;
            }
            IdentityUser userdto = new IdentityUser
            {
                Email = user.Email,
                UserName = user.UserName,
                PasswordHash = user.Password
            };
             return await _authServices.RegisterUser(userdto);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel user)
        {

            //check if te user exist


            if (ModelState.IsValid)
            {
                var userdto = new UserDto
                {
                    Email = user.Email,
                    Username = user.UserName,
                    Password = user.Password
                };
                var result = await _authServices.Login(userdto);
                if (result == true)
                {
                    var stringToken = _authServices.GeneritTokeString(userdto);
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
            var currentUserId = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(currentUserId);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password changed succesfully");
            }
            else return Unauthorized("An error occurred while attempting to change password");
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var curentuser= this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user=await _userManager.FindByIdAsync(curentuser);

            //var userdto = new ChangePasswordDto()
            //{
            //    Email = model.Email,

            //    OldPassword = model.OldPassword,
            //    NewPassword = model.NewPassword

            //};


            //var result = await _authServices.ChangePassword(userdto);



            //if (!result)
            //{
            //    return BadRequest(result);
            //}

            //return Ok();
        }
    }
}
