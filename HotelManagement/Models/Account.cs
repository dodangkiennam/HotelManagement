using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Account
    {
        [Key]
        public int AccId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleName { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
