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

        private readonly List<Institution> _institutions = new List<Institution>();
        public IReadOnlyList<Institution> AvaiilableInstitutions { get => _institutions.AsReadOnly(); }

        public int SelectedInstitutionId { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfBags { get; set; } = 1;

        public int MinNumberOfBags { get => 1; }

        public CollectionData CollectionData { get; set; }

        public DonationFormModel(MessageData messageData, CollectionData collectionData)
            :base(messageData)
        {
            CollectionData = collectionData;
        }

        public void FillCategories(IEnumerable<Category> categories)
        {
            _categories.AddRange(categories);
        }

        public void FillInstitutions(IEnumerable<Institution> institutions)
        {
            _institutions.AddRange(institutions);
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
