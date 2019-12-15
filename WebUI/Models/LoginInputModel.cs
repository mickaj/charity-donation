using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Localization;

namespace WebUI.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "invalidEmail")]
        [Display(ResourceType = typeof(ErrorMessages), Name = "emailName")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ErrorMessages), Name = "passwordName")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(ErrorMessages), Name = "rememberMe")]
        public bool RememberMe { get; set; } = true;
    }
}
