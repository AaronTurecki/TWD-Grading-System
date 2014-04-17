using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace twd_project.ViewModels
{
    public class PasswordResetModel
    {
        public string ResetToken { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [RegularExpression(@"^[^<>]+$",
ErrorMessage = "Sorry, < or > are not allowed.")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [RegularExpression(@"^[^<>]+$",
ErrorMessage = "Sorry, < or > are not allowed.")]
        public string ConfirmPassword { get; set; }
    }
}