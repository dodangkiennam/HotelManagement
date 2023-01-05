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

            modelBuilder.Entity<BookingService>().HasKey(nameof(BookingService.ServiceId), nameof(BookingService.BookingId), nameof(BookingService.OrderTime));
            modelBuilder.Entity<FeedbackImage>().HasKey(nameof(FeedbackImage.FeedbackId), nameof(FeedbackImage.ImageUrl));

            modelBuilder.Entity<Room>().HasMany(o => o.Bookings).WithOne(o => o.Room).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeImage> RoomTypeImages { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityApply> FacilityApplies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<FeedbackImage> FeedbackImages { get; set; }
    }
}
