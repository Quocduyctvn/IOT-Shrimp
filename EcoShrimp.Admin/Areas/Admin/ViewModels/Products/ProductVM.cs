using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Const;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Products
{
	public class ProductVM
	{
		public ProductVM() { }
		public int? Id { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Status { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[RegularExpression(RegexConst.FLOAT_REGEX, ErrorMessage = "Giá trị không phải số thực hợp lệ!")]
		public string OriginalPrice { get; set; } // giá gốc 
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[RegularExpression(RegexConst.FLOAT_REGEX, ErrorMessage = "Giá trị không phải số thực hợp lệ!")]
		public string SalePrice { get; set; } // giá bán
		[RegularExpression(RegexConst.INT_REGEX, ErrorMessage = "Giá trị không phải số thực hợp lệ!")]
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Quantity { get; set; }
		public string? Desc { get; set; }
		public string? Summary { get; set; }
		public int? SortOrder { get; set; }
		//-----------------------
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public int? IdCate { get; set; }
		public AppCategories? appCategories { get; set; }

		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public IFormFileCollection FormFile { get; set; }

		public List<string>? FileString { get; set; }
	}
}
