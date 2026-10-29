using Microsoft.EntityFrameworkCore;
using StayEase.Models;

namespace StayEase.Data;

public class StayEaseContext(DbContextOptions<StayEaseContext> options) : DbContext(options)
{
    public DbSet<StayUser> Users => Set<StayUser>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StayUser>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Room>().Property(r => r.PricePerNight).HasPrecision(10, 2);
        modelBuilder.Entity<Booking>().Property(b => b.TotalPrice).HasPrecision(10, 2);
        modelBuilder.Entity<Booking>().HasOne(b => b.StayUser).WithMany().HasForeignKey(b => b.StayUserId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Booking>().HasOne(b => b.Room).WithMany().HasForeignKey(b => b.RoomId).OnDelete(DeleteBehavior.Restrict);
    }
}
