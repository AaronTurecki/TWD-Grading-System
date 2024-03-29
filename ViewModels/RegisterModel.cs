﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels {
    public class RegisterModel {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }

        public string Intake { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string BCITId { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}