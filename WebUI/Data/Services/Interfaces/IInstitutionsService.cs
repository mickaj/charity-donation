using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Data.Services.Interfaces
{
    public interface IInstitutionsService
    {
        IReadOnlyList<Institution> GetInstitutions();
    }
}
