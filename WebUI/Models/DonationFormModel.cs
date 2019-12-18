using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Models
{
    public class DonationFormModel : ViewModelBase
    {
        private readonly List<Category> _categories = new List<Category>();
        public IReadOnlyList<CategoryViewModel> AvailableCategories { get => GetAvailableCategories(); }

        [Range(1, int.MaxValue)]
        public int NumberOfBags { get; set; } = 1;

        public int MinNumberOfBags { get => 1; }

        public DonationFormModel(MessageData messageData)
            :base(messageData)
        {
        }

        public void FillCategories(IEnumerable<Category> categories)
        {
            _categories.AddRange(categories);
        }

        private IReadOnlyList<CategoryViewModel> GetAvailableCategories()
        {
            var result = new List<CategoryViewModel>();
            foreach (var cat in _categories)
            {
                result.Add(new CategoryViewModel(cat));
            }
            return result;
        }
    }
}
