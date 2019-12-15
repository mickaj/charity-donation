using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Localization;

namespace WebUI.Models
{
    public class RegisterInputModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "invalidEmail")]
        [Display(ResourceType = typeof(ErrorMessages), Name = "emailName")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [StringLength(100, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ErrorMessages), Name = "passwordName")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ErrorMessages), Name = "repeatPasswordName")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "passwordsMustMatch")]
        public string ConfirmPassword { get; set; }
    }
}
