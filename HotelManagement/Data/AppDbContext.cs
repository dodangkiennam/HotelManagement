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
            modelBuilder.Entity<OccupiedRoom>().HasKey(nameof(OccupiedRoom.RoomId), nameof(OccupiedRoom.BookingId));
            modelBuilder.Entity<BookingDetail>().HasKey(nameof(BookingDetail.BookingId), nameof(BookingDetail.RoomTypeId));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
