using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("CusId")]
        public Customer Customer { get; set; }
        [Required]
        public int CusId { get; set; }

        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }
        public int? EmpId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CheckOut { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        public virtual List<OccupiedRoom> OccupiedRooms { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
