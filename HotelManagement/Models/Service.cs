using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
	public class Service
	{
        [Key]
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }

        public virtual List<BookingService>? BookingServices { get; set; }
    }
}
