using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.DataSeeders
{
	public class AppPermissionSeeder : IEntityTypeConfiguration<AppPermissions>
	{
		public void Configure(EntityTypeBuilder<AppPermissions> builder)
		{
			var now = new DateTime(2024, 3, 10);

			void SeedPermissions(string table, string groupName, params (int Id, string Code, string Desc)[] permissions)
			{
				foreach (var (Id, Code, Desc) in permissions)
				{
					builder.HasData(new AppPermissions
					{
						Id = Id,
						Code = Code,
						Table = table,
						GroupName = groupName,
						Desc = Desc,
						CreatedDate = now
					});
				}
			}

			SeedPermissions("AppUsers", "Quản lý người dùng",
				(AuthConst.AppUsers.CREATE, "CREATE", "Thêm người dùng"),
				(AuthConst.AppUsers.VIEW_LIST, "VIEW_LIST", "Xem danh sách người dùng"),
				(AuthConst.AppUsers.UPDATE, "UPDATE", "Cập nhật người dùng"),
				(AuthConst.AppUsers.DELETE, "DELETE", "Xóa người dùng"));

			SeedPermissions("AppCategories", "Quản lý danh mục",
				(AuthConst.AppCategories.CREATE, "CREATE", "Thêm danh mục"),
				(AuthConst.AppCategories.VIEW_LIST, "VIEW_LIST", "Xem danh sách danh mục"),
				(AuthConst.AppCategories.UPDATE, "UPDATE", "Cập nhật danh mục"),
				(AuthConst.AppCategories.DELETE, "DELETE", "Xóa danh mục"),
				(AuthConst.AppCategories.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết danh mục"));

			SeedPermissions("AppCateNews", "Quản lý danh mục tin tức",
				(AuthConst.AppCateNews.CREATE, "CREATE", "Thêm danh mục tin tức"),
				(AuthConst.AppCateNews.VIEW_LIST, "VIEW_LIST", "Xem danh sách danh mục tin tức"),
				(AuthConst.AppCateNews.UPDATE, "UPDATE", "Cập nhật danh mục tin tức"),
				(AuthConst.AppCateNews.DELETE, "DELETE", "Xóa danh mục tin tức"),
				(AuthConst.AppCateNews.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết danh mục tin tức"));

			SeedPermissions("AppFarms", "Quản lý trang trại",
				(AuthConst.AppFarms.CREATE, "CREATE", "Thêm trang trại"),
				(AuthConst.AppFarms.VIEW_LIST, "VIEW_LIST", "Xem danh sách trang trại"),
				(AuthConst.AppFarms.UPDATE, "UPDATE", "Cập nhật trang trại"),
				(AuthConst.AppFarms.DELETE, "DELETE", "Xóa trang trại"),
				(AuthConst.AppFarms.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết trang trại"));

			SeedPermissions("AppNews", "Quản lý tin tức",
				(AuthConst.AppNews.CREATE, "CREATE", "Thêm tin tức"),
				(AuthConst.AppNews.VIEW_LIST, "VIEW_LIST", "Xem danh sách tin tức"),
				(AuthConst.AppNews.UPDATE, "UPDATE", "Cập nhật tin tức"),
				(AuthConst.AppNews.DELETE, "DELETE", "Xóa tin tức"),
				(AuthConst.AppNews.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết tin tức"));

			SeedPermissions("AppProducts", "Quản lý sản phẩm",
				(AuthConst.AppProducts.CREATE, "CREATE", "Thêm sản phẩm"),
				(AuthConst.AppProducts.VIEW_LIST, "VIEW_LIST", "Xem danh sách sản phẩm"),
				(AuthConst.AppProducts.UPDATE, "UPDATE", "Cập nhật sản phẩm"),
				(AuthConst.AppProducts.DELETE, "DELETE", "Xóa sản phẩm"),
				(AuthConst.AppProducts.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết sản phẩm"));

			SeedPermissions("AppRequests", "Quản lý yêu cầu",
				(AuthConst.AppRequests.CREATE, "CREATE", "Thêm yêu cầu"),
				(AuthConst.AppRequests.VIEW_LIST, "VIEW_LIST", "Xem danh sách yêu cầu"),
				(AuthConst.AppRequests.UPDATE, "UPDATE", "Cập nhật yêu cầu"),
				(AuthConst.AppRequests.DELETE, "DELETE", "Xóa yêu cầu"),
				(AuthConst.AppRequests.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết yêu cầu"));

			SeedPermissions("AppRoles", "Quản lý vai trò",
				(AuthConst.AppRoles.CREATE, "CREATE", "Thêm vai trò"),
				(AuthConst.AppRoles.VIEW_LIST, "VIEW_LIST", "Xem danh sách vai trò"),
				(AuthConst.AppRoles.UPDATE, "UPDATE", "Cập nhật vai trò"),
				(AuthConst.AppRoles.DELETE, "DELETE", "Xóa vai trò"),
				(AuthConst.AppRoles.VIEW_DETAIL, "VIEW_DETAIL", "Xem chi tiết vai trò"));
		}
	}
}
