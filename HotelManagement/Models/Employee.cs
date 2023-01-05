using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [ForeignKey("AccId")]
        public virtual Account? Account { get; set; }
        public int? AccId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public double Salary { get; set; }

        public virtual List<Booking>? Bookings { get; set; }
    }
}
