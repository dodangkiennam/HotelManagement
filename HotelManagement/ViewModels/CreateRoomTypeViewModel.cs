using HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels
{
    public class Image
    {
        [Required]
        public string ImageName { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }

    public class CreateRoomTypeViewModel
    {
        public RoomType? RoomType { get; set; }
        public List<Image>? Images { get; set; }
        public List<FacilityApply>? FacilityApplies { get; set; }
    }
}
