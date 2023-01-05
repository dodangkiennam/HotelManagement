using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
	public class FeedBack
	{
		[Key]
		public int FeedbackId { get; set; }

		public string Content { get; set; }

		public double Rating { get; set; }

		public DateTime? CreateDate { get; set; }

		public List<FeedbackImage> FeedbackImages { get; set; }
	}
}
