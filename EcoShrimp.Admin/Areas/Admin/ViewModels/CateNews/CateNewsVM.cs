using EcoShrimp.Share.Enums;

namespace EcoShrimp.Admin.Areas.Admin.ViewModels.CateNews
{
	public class CateNewsVM
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string? Desc { get; set; }
		public Status Status { get; set; }
	}
}
