using EcoShrimp.Share.Const;
using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Shrimp
{
	public class ShrimpVM
	{
		// Thông tin cơ bản
		public int Id { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string WebsiteName { get; set; }
		public string? Logan { get; set; }
		public string? LogoUrl { get; set; }

		// Thông tin liên hệ
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[RegularExpression(RegexConst.PHONE_NUMBER, ErrorMessage = "Số điện thoại không hợp lệ!")]
		public string Phone { get; set; }
		[RegularExpression(RegexConst.PHONE_NUMBER, ErrorMessage = "Số điện thoại không hợp lệ!")]
		public string? SubPhone { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		[RegularExpression(RegexConst.EMAIL, ErrorMessage = "Email không đúng định dạng")]
		public string Email { get; set; }
		public string? Address { get; set; }
		public string? Map { get; set; }
		public string? OpentTime { get; set; }

		public string? FacebookUrl { get; set; }
		public string? TwitterUrl { get; set; }
		public string? InstagramUrl { get; set; }
		public string? YouTubeUrl { get; set; }

		public IFormFile? FormFile { get; set; }
	}
}
