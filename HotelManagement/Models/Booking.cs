using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("CusId")]
        public virtual Customer? Customer { get; set; }
        [Required]
        public int CusId { get; set; }

        [ForeignKey("EmpId")]
        public virtual Employee? Employee { get; set; }
        public int? EmpId { get; set; }

        [ForeignKey("RoomNo")]
        public virtual Room? Room { get; set; }
        [StringLength(10)]
        public string? RoomNo { get; set; }

        [ForeignKey("RoomTypeId")]
        public virtual RoomType? RoomType { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(1, 50)]
        public int RoomAmount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CheckOut { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        [ForeignKey("FeedbackId")]
        public virtual FeedBack? FeedBack { get; set; }
        public int? FeedbackId { get; set; }

        public virtual List<BookingService>? BookingServices { get; set; }
    }
}
