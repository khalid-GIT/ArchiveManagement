using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Dtos
{
    public class UserDto
    {

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;


    }
    //public class ChangePasswordDto
    //{
    //    public string Email { get; set; } = string.Empty;
    //    public string OldPassword { get; set; } = string.Empty;
    //    public string NewPassword { get; set; } = string.Empty;
    //    public string Username { get; set; } = string.Empty;
    //    public string ConfirmPassword { get; set; }

    //}

}
