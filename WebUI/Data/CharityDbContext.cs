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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryDonation>()
                .HasKey(t => new { t.CategoryId, t.DonationId });

            builder.Entity<CategoryDonation>()
                .HasOne(cd => cd.Category)
                .WithMany(c => c.CategoryDonations)
                .HasForeignKey(cd => cd.CategoryId);

            builder.Entity<CategoryDonation>()
                .HasOne(cd => cd.Donation)
                .WithMany(d => d.CategoryDonations)
                .HasForeignKey(cd => cd.DonationId);
        }
    }
}
