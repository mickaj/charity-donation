using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.Services.Interfaces
{
    public interface IDonationsService
    {
        int GetNumberOfRecievers();

        int GetNumberOfDonatedBags();
    }
}
