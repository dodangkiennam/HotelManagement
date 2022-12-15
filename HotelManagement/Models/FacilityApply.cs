using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class FacilityApply
    {
        [ForeignKey("FacId")]
        public virtual Facility Facility { get; set; }
        [Required]
        public int FacId { get; set; }

        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Amount { get; set; }
    }
}
