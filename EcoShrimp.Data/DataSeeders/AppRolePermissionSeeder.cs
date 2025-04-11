//using EcoShrimp.Data.Entities;
//using EcoShrimp.Share.Const;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EcoShrimp.Data.DataSeeders
//{
//	public class AppRolePermissionSeeder : IEntityTypeConfiguration<AppRolePermission>
//	{
//		public void Configure(EntityTypeBuilder<AppRolePermission> builder)
//		{
//			var now = new DateTime(year: 2024, month: 3, day: 10);
//			var roleId = 1; // Admin role ID
//			int idCounter = 1; // Bắt đầu từ ID 1

//			var permissions = new List<int>
//			{
//                // AppCategories
//                AuthConst.AppCategories.VIEW_LIST,
//				AuthConst.AppCategories.CREATE,
//				AuthConst.AppCategories.UPDATE,
//				AuthConst.AppCategories.DELETE,
//				AuthConst.AppCategories.VIEW_DETAIL,

//                // AppCateNews
//                AuthConst.AppCateNews.VIEW_LIST,
//				AuthConst.AppCateNews.CREATE,
//				AuthConst.AppCateNews.UPDATE,
//				AuthConst.AppCateNews.DELETE,
//				AuthConst.AppCateNews.VIEW_DETAIL,

//                // AppFarms
//                AuthConst.AppFarms.VIEW_LIST,
//				AuthConst.AppFarms.CREATE,
//				AuthConst.AppFarms.UPDATE,
//				AuthConst.AppFarms.DELETE,
//				AuthConst.AppFarms.VIEW_DETAIL,

//                // AppNews
//                AuthConst.AppNews.VIEW_LIST,
//				AuthConst.AppNews.CREATE,
//				AuthConst.AppNews.UPDATE,
//				AuthConst.AppNews.DELETE,
//				AuthConst.AppNews.VIEW_DETAIL,

//                // AppProducts
//                AuthConst.AppProducts.VIEW_LIST,
//				AuthConst.AppProducts.CREATE,
//				AuthConst.AppProducts.UPDATE,
//				AuthConst.AppProducts.DELETE,
//				AuthConst.AppProducts.VIEW_DETAIL,

//                // AppRequests
//                AuthConst.AppRequests.VIEW_LIST,
//				AuthConst.AppRequests.CREATE,
//				AuthConst.AppRequests.UPDATE,
//				AuthConst.AppRequests.DELETE,
//				AuthConst.AppRequests.VIEW_DETAIL,

//                // AppRoles
//                AuthConst.AppRoles.VIEW_LIST,
//				AuthConst.AppRoles.CREATE,
//				AuthConst.AppRoles.UPDATE,
//				AuthConst.AppRoles.DELETE,
//				AuthConst.AppRoles.VIEW_DETAIL,

//                // AppUsers
//                AuthConst.AppUsers.VIEW_LIST,
//				AuthConst.AppUsers.CREATE,
//				AuthConst.AppUsers.UPDATE,
//				AuthConst.AppUsers.DELETE,
//				AuthConst.AppUsers.VIEW_DETAIL
//			};

//			var rolePermissions = new List<AppRolePermission>();
//			foreach (var PerId in permissions)
//			{
//				rolePermissions.Add(new AppRolePermission
//				{
//					Id = idCounter++,
//					IdRole = roleId,
//					IdPermission = PerId,
//					CreatedDate = now
//				});
//			}

//			builder.HasData(rolePermissions);
//		}
//	}
//}
