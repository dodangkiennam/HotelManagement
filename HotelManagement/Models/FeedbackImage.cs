using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
	public class FeedbackImage
	{
        [ForeignKey("FeedbackId")]
        public virtual FeedBack? FeedBack { get; set; }
        public int FeedbackId { get; set; }

        public string ImageUrl { get; set; }
    }
}
