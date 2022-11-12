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

        [MaxLength(10)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
