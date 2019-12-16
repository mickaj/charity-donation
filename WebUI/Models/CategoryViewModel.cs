using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public bool IsChecked { get; set; } = false;

        public CategoryViewModel(Category category)
        {
            Category = category;
        }
    }
}
