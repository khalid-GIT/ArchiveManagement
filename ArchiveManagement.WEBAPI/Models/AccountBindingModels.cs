
using System.ComponentModel.DataAnnotations;
public class LoginModel
{
     [Required]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

     [Required]
    [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }
  //  public string UserName { get; set; } = string.Empty;
}
public class RemoveLoginModel
{
    [Required]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    //[Required]
    //[StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
    //[DataType(DataType.Password)]
    //[Display(Name = "Mot de passe")]
    //public string Password { get; set; }
    //  public string UserName { get; set; } = string.Empty;
}
public class RegisterBindingModel
{
    [Required]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }
    public string ?Role { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmer le mot de passe ")]
    [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
}

public class RemoveLoginBindingModel
{
    [Required]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    
}

public class SetPasswordBindingModel
{
    [Required]
    [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nouveau mot de passe")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmer le nouveau mot de passe")]
    [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
}
public class ChangePasswordBindingModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe actuel")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nouveau mot de passe")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmer le nouveau mot de passe")]
    [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
}