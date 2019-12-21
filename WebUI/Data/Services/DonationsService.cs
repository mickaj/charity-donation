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

        public IReadOnlyList<Category> GetCategories()
        {
            return _context.Categories.ToList().AsReadOnly();
        }

        public void AddDonation(string userId, int institutionId, string street, string city, string zipCode, DateTime pickUpDate, string notes, int bagsQty, IEnumerable<int> categoryIds)
        {
            var institution = _context.Institutions.Single(i => i.Id == institutionId);
            var categories = _context.Categories.Where(c => categoryIds.Contains(c.Id));

            var donation = new Donation
            {
                CharityUserId = userId,
                Institution = institution,
                Quantity = bagsQty,
                Street = street,
                City = city,
                ZipCode = zipCode,
                PickUpDate = pickUpDate,
                PickUpComment = string.IsNullOrWhiteSpace(notes) ? string.Empty : notes
            };

            var cds = new List<CategoryDonation>();
            foreach(var cat in categories)
            {
                cds.Add(new CategoryDonation { Donation = donation, Category = cat });
            }
            donation.CategoryDonations = cds;

            _context.Donations.Add(donation);
            _context.SaveChanges();
        }
    }
}
