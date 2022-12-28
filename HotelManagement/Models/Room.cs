using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Room
    {
        [Key]
        [StringLength(10)]
        public string RoomNo { get; set; }

        [ForeignKey("RoomTypeId")]
        public RoomType? RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        public string? Description { get; set; }

        public virtual List<OccupiedRoom>? OccupiedRooms { get; set; }
    }
}
