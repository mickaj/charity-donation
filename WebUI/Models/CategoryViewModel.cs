using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsChecked { get; set; } = false;

        public CategoryViewModel()
        {
        }
        
        public CategoryViewModel(Category category)
        {
            CategoryId = category.Id;
            CategoryName = category.Name;
        }
    }
}
