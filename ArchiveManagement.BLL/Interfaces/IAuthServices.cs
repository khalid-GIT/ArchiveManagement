using ArchiveManagement.BLL.Dtos;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArchiveManagement.BLL.Interfaces
{
    public interface IAuthServices
    {
        Task<bool> RegisterUser(IdentityUser user);

      Task<bool>  Login(UserDto user);
        string GeneritTokeString(UserDto userdto);
        //Task<bool> ChangePassword(ChangePasswordDto chpassworddto);


    }
}
