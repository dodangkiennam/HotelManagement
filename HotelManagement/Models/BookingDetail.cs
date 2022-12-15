using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class BookingDetail
    {
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
        [Required]
        public int BookingId { get; set; }

        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(1, 50)]
        public int RoomAmount { get; set; }
    }
}
