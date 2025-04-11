using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Views.Shared.Components.Permission
{
	public class PermissionViewComponent : ViewComponent
	{
		protected readonly ApplicationDbContext _DbContext;

		public PermissionViewComponent(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var role = _DbContext.AppPermissions
				.AsEnumerable()
				.GroupBy(x => x.GroupName)
				.ToList();

			return View(role);
		}
	}
}
