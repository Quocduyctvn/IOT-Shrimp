using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.DataSeeders
{
	public class AppRolesSeeder : IEntityTypeConfiguration<AppRoles>
	{
		public void Configure(EntityTypeBuilder<AppRoles> builder)
		{
			var now = new DateTime(year: 2024, month: 3, day: 10);

			var admin = new AppRoles
			{
				Id = 1,
				Name = "Quản trị hệ thống",
				Desc = "Quản trị toàn bộ hệ thống",
				CreatedDate = now,
			};

			builder.HasData(admin);
		}
	}
}
