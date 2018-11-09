using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillApp.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage ="Campo requerido")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Recordar este navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo requerido") ]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El Email no tiene el formato correcto.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        [EmailAddress(ErrorMessage = "El Email no tiene el formato correcto.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [StringLength(100, ErrorMessage = "El {0} debe tener por lo menos {2} caracteres de largo.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Password")]
        [Compare("Password", ErrorMessage = "El password y la confirmacion de password no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "El Email no tiene el formato correcto.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener por lo menos {2} caracteres de largo.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "El password y la confirmacion de password no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "El Email no tiene el formato correcto.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
