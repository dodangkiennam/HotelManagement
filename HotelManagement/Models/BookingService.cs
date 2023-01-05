using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
	public class BookingService
	{
        [ForeignKey("ServiceId")]
        public virtual Service? Service { get; set; }
        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }
        [Required]
        public int? BookingId { get; set; }

        public DateTime? OrderTime { get; set; }

        [Required]
        [Range(1, 10)]
        public int Amount { get; set; }
    }
}
