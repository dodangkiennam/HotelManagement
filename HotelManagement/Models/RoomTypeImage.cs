using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class RoomTypeImage
    {
        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
