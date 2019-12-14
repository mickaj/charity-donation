using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;
using WebUI.Data.Services.Interfaces;

namespace WebUI.Data.Services
{
    public class DonationsService : ServiceBase, IDonationsService
    {
        public DonationsService(CharityDbContext context)
            : base(context)
        {
        }

        public int GetNumberOfDonatedBags()
        {
            return _context.Donations.Sum(d => d.Quantity);
        }

        public int GetNumberOfRecievers()
        {
            return _context.Donations.Select(r => r.Institution.Id).Distinct().Count();
        }
    }
}
