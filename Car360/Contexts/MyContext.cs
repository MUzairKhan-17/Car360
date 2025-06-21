using Car360.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Car360.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) :base(options){ }

        public DbSet<Sign> tbl_sign { get; set; }
        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        public DbSet<Buy> tbl_buy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buy>()
                .HasOne(a => a.Signone)
                .WithMany(b => b.Buyone)
                .HasForeignKey(c => c.User_ID);
        }
    }
}
