using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Data.Services.Interfaces
{
    public interface IDonationsService
    {
        int GetNumberOfRecievers();

        int GetNumberOfDonatedBags();

        IReadOnlyList<Category> GetCategories();

        void AddDonation(string userId, int institutionId, string street, string city, string zipCode, DateTime pickUpDate, string notes, int bagsQty, IEnumerable<int> categoryIds);
    }
}
