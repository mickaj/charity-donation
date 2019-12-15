using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Interfaces;

namespace WebUI.Models
{
    public class IndexViewModel : IMessagingFormData
    {
        public WhoWeHelpViewModel WhoWeHelpViewModel { get; set; }

        public StatsViewModel StatsViewModel { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Message { get; set; }


        public IndexViewModel(WhoWeHelpViewModel whoWeHelpViewModel, StatsViewModel statsViewModel)
        {
            WhoWeHelpViewModel = whoWeHelpViewModel;
            StatsViewModel = statsViewModel;
        }
    }
}
