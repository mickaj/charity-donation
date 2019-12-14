using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;
using WebUI.Data.Services.Interfaces;

namespace WebUI.Data.Services
{
    public class InstitutionsService : ServiceBase, IInstitutionsService
    {
        public InstitutionsService(CharityDbContext context)
            : base(context)
        {
        }

        public IReadOnlyList<Institution> GetInstitutions()
        {
            return _context.Institutions.ToList().AsReadOnly();
        }

        public IReadOnlyList<Institution> GetRandomInstitutions(int qty)
        {
            var availableCount = _context.Institutions.Count();
            if(availableCount <= qty)
            {
                return _context.Institutions.ToList().AsReadOnly();
            }
            else
            {
                return _context.Institutions.OrderBy(rnd => Guid.NewGuid()).Take(qty).ToList().AsReadOnly();
            }
        }

        public int GetInstitutionsCount()
        {
            return _context.Institutions.Count();
        }
    }
}
