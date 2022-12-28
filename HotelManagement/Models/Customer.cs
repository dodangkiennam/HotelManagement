using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    public class Customer
    {
        [Key]
        public int CusId { get; set; }

        [ForeignKey("AccId")]
        public virtual Account Account { get; set; }
        public int? AccId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Citizen Card ID")]
        [MaxLength(20)]
        public string CitizencardId { get; set; }

        [DisplayName("Country Code")]
        [MaxLength(10)]
        public string CountryCode { get; set; }

        public virtual List<Booking>? Bookings { get; set; }
    }
}
