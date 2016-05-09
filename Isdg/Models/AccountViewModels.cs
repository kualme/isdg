using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Isdg.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]        
        public string Email { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Receive newsletter")]
        public bool ReceiveNewsletter { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}
    //
    //public class VerifyCodeViewModel
    //{
    //    [Required]
    //    public string Provider { get; set; }
    //
    //    [Required]        
    //    public string Code { get; set; }
    //    public string ReturnUrl { get; set; }
    //
    //    [Display(Name = "Remember browser")]
    //    public bool RememberBrowser { get; set; }
    //
    //    public bool RememberMe { get; set; }
    //}

    public class ForgotViewModel
    {
        [Required]        
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]        
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Receive newsletter")]
        public bool ReceiveNewsletter { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]        
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]        
        public string Email { get; set; }
    }
}
