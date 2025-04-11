using EcoShrimp.Share.Const;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Users
{
	public class UserVM
	{
		[Required(ErrorMessage = "Thuộc tính là bắt buột!!")]
		public string Name { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột!!")]
		public string Phone { get; set; }
		[RegularExpression(RegexConst.PASSWORD, ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số, ký tự đặc biệt và ≥ 6 ký tự.")]
		[Required(ErrorMessage = "Thuộc tính là bắt buột!!")]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
		[Required(ErrorMessage = "Thuộc tính là bắt buột!!")]
		public string Cfm_Password { get; set; }

		[Required(ErrorMessage = "Thuộc tính là bắt buột!!")]
		public int IdRole { get; set; }
	}
}
