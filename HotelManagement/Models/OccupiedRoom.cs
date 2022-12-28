using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class OccupiedRoom
    {
        [ForeignKey("RoomNo")]
        public virtual Room? Room { get; set; }
        [StringLength(10)]
        public string? RoomNo { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }
        public int? BookingId { get; set; }
    }
}
