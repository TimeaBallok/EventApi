using EventAPI.model;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Infrastructure.Database
{
    public class EventDB : DbContext
    {
        public EventDB(DbContextOptions<EventDB> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurer relationerne
            //modelBuilder.Entity<Event>()
            //    .HasMany(e => e.Bookings)
            //    .WithOne(b => b.Event)
            //    .HasForeignKey(b => b.EventId);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Bookings)
            //    //.WithOne(b => b.User)
            //    .HasForeignKey(b => b.UserId);
        }
    }
}
