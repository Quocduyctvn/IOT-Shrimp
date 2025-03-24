using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Requests
{
	public class FeedbackVM
	{
		public FeedbackVM() { }

		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public int IdRequest { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Subject { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Body { get; set; }
	}
}
