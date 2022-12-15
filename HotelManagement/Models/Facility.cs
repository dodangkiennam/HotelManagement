using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Facility
    {
        [Key]
        public int FacId { get; set; }
        
        public string Name { get; set; }
        
        public string ImageUrl { get; set; }
        
        public virtual List<FacilityApply>? FacilityApplies { get; set; }
    }
}
