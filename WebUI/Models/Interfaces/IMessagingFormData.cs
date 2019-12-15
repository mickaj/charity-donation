using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Interfaces
{
    public interface IMessagingFormData
    {
        [Required]
        string Name { get; set; }

        [Required]
        string LastName { get; set; }

        [Required]
        string Message { get; set; }
    }
}
