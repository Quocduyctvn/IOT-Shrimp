namespace EcoShrimp.Admin.Areas.Admin.ViewModels.Roles
{
	public class UpdateRoleVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public string IdPermission { get; set; }

		public string DeletedIdPermission { get; set; }
		public string AddedIdPermission { get; set; }
	}
}
