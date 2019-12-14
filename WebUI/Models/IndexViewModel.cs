using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class IndexViewModel
    {
        public WhoWeHelpViewModel WhoWeHelpViewModel { get; set; }

        public StatsViewModel StatsViewModel { get; set; }

        public IndexViewModel(WhoWeHelpViewModel whoWeHelpViewModel, StatsViewModel statsViewModel)
        {
            WhoWeHelpViewModel = whoWeHelpViewModel;
            StatsViewModel = statsViewModel;
        }
    }
}
