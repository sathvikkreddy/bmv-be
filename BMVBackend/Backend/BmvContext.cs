using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class BmvContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookedSlot> BookedSlots { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"server=.\sqlexpress;initial catalog=bmv;user id=sa;password=Pass@123;trustservercertificate=true");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    var customer = modelBuilder.Entity<Customer>();
        //}
    }
}
