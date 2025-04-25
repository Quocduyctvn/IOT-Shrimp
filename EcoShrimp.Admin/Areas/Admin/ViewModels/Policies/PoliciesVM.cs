using EcoShrimp.Share.Enums;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Policies
{
	public class PoliciesVM
	{
		public int? Id { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Name { get; set; }
		public string? Path { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Summary { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Content { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public IFormFile FormFile { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public Status Status { get; set; }
	}
}
