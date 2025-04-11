using System.ComponentModel.DataAnnotations;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Roles
{
	public class AddRoleVM
	{
		[Required(ErrorMessage = "Tên Vai trò là bắt buột")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Thuộc tính là bắt buột")]
		public string Desc { get; set; }
		public string IdPermission { get; set; }
	}
}
