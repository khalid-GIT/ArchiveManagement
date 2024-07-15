﻿using ArchiveManagement.DAL.Entities;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveManagement.BLL.Dtos;
using Org.BouncyCastle.Asn1.Pkcs;
using ArchiveManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http;
//using Fluent.Infrastructure.FluentModel;
//using Microsoft.AspNet.Identity;

namespace ArchiveManagement.BLL.Implementations
{
    public class AuthServices : IAuthServices
    {
        // protected  AuthDal _authDal = new AuthDal();
        protected UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _config;

        public AuthServices(UserManager<IdentityUser> usermanager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = usermanager;
            _config = config;
            _roleManager = roleManager;
        }
        public async Task<bool> RegisterUser(IdentityUser userdto, string password)
        {
            var result = await _userManager.CreateAsync(userdto, password);

            if (result.Succeeded)
            {
                //Ajout user au Claims
                //var MyClaimTypes = new ClaimsPrincipal(new ClaimsIdentity(await _userManager.GetClaimsAsync(userdto)));
                // await _userManager.AddClaimAsync(userdto, new System.Security.Claims.Claim(MyClaimTypes.Stuff, MyClaimTypes.Whatever));
                return true;
            }
            else { return false; }
        }
        public async Task<bool> Login(IdentityUser user, string password)
        {

            var _identityUser = await _userManager.FindByEmailAsync(user.Email);


            if (_identityUser == null)
            {
                return false;
            }
            else
            {
                var _roleUser = await _userManager.GetRolesAsync(_identityUser);
            }
            return await _userManager.CheckPasswordAsync(_identityUser, password);
        }

        public string GeneritTokeString(IdentityUser userdto, string password)
        {

        //    var _role = _userManager.GetRolesAsync(userdto);
          var userRoles = _userManager.GetRolesAsync(userdto).ToString();

        


            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,userdto.UserName),
               new Claim(ClaimTypes.Role,userRoles!=null?userRoles:"Ghuist"),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("jwt:key").Value));

            var SigningCread = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            SecurityToken stringtoken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("jwt:Issuer").Value, audience: _config.GetSection("jwt:Audience").Value,
                signingCredentials: SigningCread
                );
            string tokenstring = new JwtSecurityTokenHandler().WriteToken(stringtoken);
            return tokenstring;


        }


        //public async Task<bool> ChangePassword(ChangePasswordDto chpassworddto)
        //{

        //    //IdentityUser user = await UserManager.FindByIdAsync(UserManager.Users.Identity.GetUserId());
        //    //var curentuser = User.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;

        //    //var user=await _userManager.FindByIdAsync(curentuser);

        //    //IdentityResult result = await _userManager.ChangePasswordAsync(user, chpassworddto.OldPassword,
        //    //       chpassworddto.NewPassword);
        //    //return result;
        // //   var user = await _userManager.FindByIdAsync(chpassworddto.Email);
        //    var _identityUser = await _userManager.FindByEmailAsync(chpassworddto.Email);

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(_identityUser);

        //    var result = await _userManager.ResetPasswordAsync(_identityUser, token, chpassworddto.NewPassword);


        //    if (_identityUser == null)
        //    {
        //        return false;
        //    }
        //    _identityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(_identityUser, chpassworddto.NewPassword);
        //    var results = await _userManager.UpdateAsync(_identityUser);
        //    if (!results.Succeeded)
        //    {
        //        //throw exception......
        //    }
        //    return true;
        //}
    }
}
