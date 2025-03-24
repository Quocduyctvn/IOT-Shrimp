using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Views.Shared.Components.MainNavBar
{
	public class MainNavBarViewComponent : ViewComponent
	{
		protected readonly ApplicationDbContext _HotelDbContext;
		public MainNavBarViewComponent(ApplicationDbContext DbContext)
		{
			_HotelDbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var idGroupClaim = HttpContext.User.FindFirst("IdGroup")?.Value;

			var navBar = new NavBarViewModel();
			navBar.Items.AddRange(new MenuItem[]
			{
				new MenuItem
				{
					DisplayText = "Quản lý khách hàng",
					Icon = "far fa-chart-bar",
					ChildrenItems = new MenuItem[]
					{
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminFarm",
							DisplayText = "Quản lý trang trại",
							Icon = "fas fa-calendar-check"
						}
					}
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminCategories",
					DisplayText = "Quản lý loại sản phẫm",
					Icon = "fas fa-bath",
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminProduct",
					DisplayText = "Quản lý Sản phẫm",
					Icon = "fas fa-bath",
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminRequest",
					DisplayText = "Quản lý yêu cầu",
					Icon = "fas fa-bath",
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminCateNews",
					DisplayText = "Quản lý loại tin",
					Icon = "fas fa-bath",
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminNews",
					DisplayText = "Quản lý tin tức",
					Icon = "fas fa-bath",
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				}


			});
			//TempData["Hotel"] = _HotelDbContext.AppHotel.Where(x => x.IdGroup == int.Parse(idGroupClaim)).FirstOrDefault();
			//if (TempData["Hotel"] as AppHotels == null)
			//{
			//	return View("Index");
			//}
			return View(navBar);
		}
	}
}
