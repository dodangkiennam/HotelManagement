using HotelManagement.Models;

namespace HotelManagement.ViewModels
{
	public class CreateFacilityViewModel
	{
		public Facility Facility { get; set; }
		public IFormFile ImageFile { get; set; }
	}
}
