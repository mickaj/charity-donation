using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.Services
{
    public abstract class ServiceBase
    {
        protected readonly CharityDbContext _context;

        public ServiceBase(CharityDbContext context)
        {
            _context = context;
        }
    }
}
