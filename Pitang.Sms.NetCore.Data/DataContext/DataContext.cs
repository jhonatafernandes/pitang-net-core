using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Entities.Models;

namespace Pitang.Sms.NetCore.Data.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Storie> Stories { get; set; }

        public DbSet<Messages> Messages { get; set; }

        public DbSet<HistoricPassword> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.Username })
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.Email })
                .IsUnique(true);
        }
    }
}
