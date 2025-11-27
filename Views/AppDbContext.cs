using Microsoft.EntityFrameworkCore;
using BloodDonation.Models;

namespace BloodDonation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}
