using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	internal class AppCateNewConfig : IEntityTypeConfiguration<AppCateNews>
	{
		public void Configure(EntityTypeBuilder<AppCateNews> builder)
		{
			builder.ToTable("app_cateNew");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
		}
	}
}
