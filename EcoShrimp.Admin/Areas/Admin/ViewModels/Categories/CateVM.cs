using EcoShrimp.Share.Enums;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Categories
{
	public class CateVM
	{
		public int? Id { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Name { get; set; }
		public string? Desc { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public Status Status { get; set; }
	}
}
