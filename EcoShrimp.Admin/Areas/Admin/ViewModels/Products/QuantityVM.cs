using EcoShrimp.Share.Const;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Products
{
	public class QuantityVM
	{
		public int Id { get; set; }
		[RegularExpression(RegexConst.INT_REGEX, ErrorMessage = "Giá trị không phải số thực hợp lệ!")]
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public int Quantity { get; set; }
	}
}
