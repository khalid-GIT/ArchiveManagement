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
        Task<bool> RegisterUser(IdentityUser user,string password);

      Task<bool>  Login(IdentityUser user,string password);
        string GeneritTokeString(IdentityUser user, string password);
        //Task<bool> ChangePassword(ChangePasswordDto chpassworddto);
   


    }
}
