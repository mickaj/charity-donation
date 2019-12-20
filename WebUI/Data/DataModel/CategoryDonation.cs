using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.DataModel
{
    public class CategoryDonation
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int DonationId { get; set; }
        public Donation Donation { get; set; }
    }
}
