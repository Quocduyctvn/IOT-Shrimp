using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppPerConfig : IEntityTypeConfiguration<AppPermissions>
	{
		public void Configure(EntityTypeBuilder<AppPermissions> builder)
		{
			builder.ToTable("app_permissions");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength((int)MaxLength.MAX_128);
			builder.Property(x => x.Table).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.GroupName).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_512);
		}
	}
}
