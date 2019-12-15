using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Localization;

namespace WebUI.Models
{
    public class MessageData
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [Display(ResourceType = typeof(ErrorMessages), Name = "firstName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [Display(ResourceType = typeof(ErrorMessages), Name = "lastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "fieldCannotBeEmpty")]
        [Display(ResourceType = typeof(ErrorMessages), Name = "message")]
        public string Message { get; set; }
    }
}
