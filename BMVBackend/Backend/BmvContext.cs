using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class BmvContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookedSlot> BookedSlots { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"server=.\sqlexpress;initial catalog=bmv;user id=sa;password=Pass@123;trustservercertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Provider)
                .HasForeignKey(b => b.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Venues)
                .WithOne(v => v.Provider)
                .HasForeignKey(v => v.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Bookings)
                .WithOne(b => b.Venue)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Slots)
                .WithOne(s => s.Venue)
                .HasForeignKey(s => s.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.BookedSlots)
                .WithOne(bs => bs.Venue)
                .HasForeignKey(bs => bs.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Slots)
                .WithOne(s => s.Booking)
                .HasForeignKey(s => s.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.BookedSlots)
                .WithOne(bs => bs.Booking)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Slot>()
                .HasMany(s => s.BookedSlots)
                .WithOne(bs => bs.Slot)
                .HasForeignKey(bs => bs.SlotId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
