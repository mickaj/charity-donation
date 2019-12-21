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
        public List<CategoryViewModel> AvailableCategories { get; set; } = new List<CategoryViewModel>();

        public string CategoriesString { get; set; }

        private readonly List<Institution> _institutions = new List<Institution>();
        public IReadOnlyList<Institution> AvaiilableInstitutions { get => _institutions.AsReadOnly(); }

        public int SelectedInstitutionId { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfBags { get; set; } = 1;

        public int MinNumberOfBags { get => 1; }

        public CollectionData CollectionData { get; set; }

        public DonationFormModel()
            :base(new MessageData())
        {
            CollectionData = new CollectionData();
        }
        
        public DonationFormModel(MessageData messageData, CollectionData collectionData)
            :base(messageData)
        {
            CollectionData = collectionData;
        }

        public void FillCategories(IEnumerable<Category> categories)
        {
            foreach (var cat in categories)
            {
                AvailableCategories.Add(new CategoryViewModel(cat));
            }
        }

        public void FillInstitutions(IEnumerable<Institution> institutions)
        {
            _institutions.AddRange(institutions);
        }
    }
}
