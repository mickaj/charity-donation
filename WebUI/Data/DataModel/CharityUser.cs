using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.DataModel
{
    public class CharityUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public List<Donation> Donations { get; set; }
    }
}
