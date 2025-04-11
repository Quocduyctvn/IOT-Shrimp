using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppRolePerConfig : IEntityTypeConfiguration<AppRolePermission>
	{
		public void Configure(EntityTypeBuilder<AppRolePermission> builder)
		{
			builder.ToTable("app_role_per");
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.appRole)
				.WithMany(x => x.appRolePermissions)
				.HasForeignKey(x => x.IdRole)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(x => x.appPermission)
				.WithMany(x => x.appRolePermissions)
				.HasForeignKey(x => x.IdPermission)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}