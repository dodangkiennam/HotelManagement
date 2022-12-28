using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            //nothing to do here
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomTypeImage>().HasKey(nameof(RoomTypeImage.RoomTypeId), nameof(RoomTypeImage.ImageUrl));
            modelBuilder.Entity<FacilityApply>().HasKey(nameof(FacilityApply.FacId), nameof(FacilityApply.RoomTypeId));
            modelBuilder.Entity<OccupiedRoom>().HasKey(nameof(OccupiedRoom.RoomNo), nameof(OccupiedRoom.BookingId));
            modelBuilder.Entity<Room>().HasMany(o => o.OccupiedRooms).WithOne(o => o.Room).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeImage> RoomTypeImages { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityApply> FacilityApplies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<OccupiedRoom> OccupiedRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
