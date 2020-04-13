using Microsoft.EntityFrameworkCore;
using sms_pitang_netcore.Models;

namespace sms_pitang_netcore.Data
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
    }
}
