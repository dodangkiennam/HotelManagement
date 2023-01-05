using HotelManagement.Models;

namespace HotelManagement.ViewModels
{
    public class CreateServiceViewModel
    {
        public Service Service { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
