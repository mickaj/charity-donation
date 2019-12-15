using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Data
{
    public class CharityDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Donation> Donations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public CharityDbContext(DbContextOptions<CharityDbContext> options)
            : base (options)
        {
        }
    }
}
