using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.DataModel
{
    public class Donation
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Required]
        public Institution Institution { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [NotMapped]
        public string PickUpTime { get => PickUpDate.ToString("HH:mm"); }

        public string PickUpComment { get; set; }

        public List<CategoryDonation> CategoryDonations { get; set; }

        public string CharityUserId { get; set; }
    }
}
