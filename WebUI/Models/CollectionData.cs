using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CollectionData
    {
        [Required]
        public string Street { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string ZipCode { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Date { get; set; }
        
        [Required]
        public string Time { get; set; }
        
        public string Notes { get; set; }
    }
}
