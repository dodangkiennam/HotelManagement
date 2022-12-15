using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class OccupiedRoom
    {
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        [Required]
        public int RoomId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
        [Required]
        public int BookingId { get; set; }
    }
}
