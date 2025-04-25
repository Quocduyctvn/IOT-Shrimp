using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Views.Shared.Components.MainNavBar
{
	public class MainNavBarViewComponent : ViewComponent
	{
		protected readonly ApplicationDbContext _DbContext;
		public MainNavBarViewComponent(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TempData["shrimp"] = _DbContext.AppShrimps.FirstOrDefault();

			var idGroupClaim = HttpContext.User.FindFirst("IdGroup")?.Value;

			var navBar = new NavBarViewModel();
			navBar.Items.AddRange(new MenuItem[]	//<i class="fas fa-boxes"></i>
			{
				new MenuItem
				{
					DisplayText = "Quản lý khách hàng",
					Icon = "fas fa-users",
					ChildrenItems = new MenuItem[]
					{
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminFarm",
							DisplayText = "Quản lý trang trại",
							Icon = "fas fa-vector-square"
						}
					}
				},
				new MenuItem
				{
					DisplayText = "Giải pháp công nghệ",
					Icon = "fas fa-project-diagram",
					ChildrenItems = new MenuItem[]
					{
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminCategories",
							DisplayText = "Quản lý loại sản phẫm",
							Icon = "fas fa-boxes",
						},
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminProduct",
							DisplayText = "Quản lý Sản phẫm",
							Icon = "fas fa-crop",
						},
					}
				},
				new MenuItem
				{
					DisplayText = "Quản lý bài viết",
					Icon = "fas fa-outdent",
					ChildrenItems = new MenuItem[]
					{
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminCateNews",
							DisplayText = "Quản lý loại tin",
							Icon = "fas fa-list-ul",
							//Permission = AuthConst.AppAmenity.VIEW_LIST, 
						},
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminNews",
							DisplayText = "Quản lý tin tức",
							Icon = "fas fa-layer-group",
							//Permission = AuthConst.AppAmenity.VIEW_LIST,
						},
					}
				},
				new MenuItem
				{
					DisplayText = "Tài khoản&Phân quyền",
					Icon = "fas fa-users-cog",
					ChildrenItems = new MenuItem[]
					{
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminUsers",
							DisplayText = "Quản lý người dùng",
							Icon = "fas fa-user-alt",
							//Permission = AuthConst.AppAmenity.VIEW_LIST,
						},
						new MenuItem
						{
							Action = "Index",
							Controller = "AdminRoles",
							DisplayText = "Quản lý vai trò",
							Icon = "fas fa-user-tag",
							//Permission = AuthConst.AppAmenity.VIEW_LIST,
						},
					}
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminPolicies",
					DisplayText = "Quản lý chính sách",
					Icon = "fas fa-shield-alt", 
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminRequest",
					DisplayText = "Quản lý yêu cầu",
					Icon = "fas fa-retweet", 
					//Permission = AuthConst.AppAmenity.VIEW_LIST,
				},
				new MenuItem
				{
					Action = "Index",
					Controller = "AdminWebsite",
					DisplayText = "Quản lý website",
					Icon = "fas fa-cogs",
					//Permission = AuthConst.AppAmenity.VIEW_LIST, 
				},
				new MenuItem
				{
					Action = "SendCode",
					Controller = "AdminChangepass",
					DisplayText = "Đổi mật khẩu",
					Icon = "fas fa-key",
					//Permission = AuthConst.AppAmenity.VIEW_LIST, 
				},


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
