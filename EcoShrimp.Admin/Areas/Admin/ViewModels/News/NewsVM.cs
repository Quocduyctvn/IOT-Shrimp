using EcoShrimp.Share.Enums;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.News
{
	public class NewsVM
	{
		public NewsVM() { }

		public int? Id { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Title { get; set; }
		public string? Path { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Summary { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Content { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public Status Status { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public IFormFile FormFile { get; set; }
		public int IdCateNews { get; set; }
	}
}
