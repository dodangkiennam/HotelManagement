using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HotelManagement.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int? Quantity { get; set; }

        public int MaxAdult { get; set; }

        public int MaxChild { get; set; }

        public string? Description { get; set; }

        public virtual List<RoomTypeImage>? RoomTypeImages { get; set; }
        public virtual List<Booking>? Bookings { get; set; }
        public virtual List<FacilityApply>? FacilityApplies { get; set; }
        public virtual List<Room>? Rooms { get; set; }
    }
}
