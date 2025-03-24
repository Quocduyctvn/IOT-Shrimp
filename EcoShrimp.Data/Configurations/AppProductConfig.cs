using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppProductConfig : IEntityTypeConfiguration<AppProducts>
	{
		public void Configure(EntityTypeBuilder<AppProducts> builder)
		{
			builder.ToTable("app_products");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength((int)MaxLength.MAX_32);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Summary).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Video).HasMaxLength((int)MaxLength.MAX_512);

			builder.HasOne(x => x.appCategory)
					.WithMany(x => x.appProducts)
					.HasForeignKey(x => x.IdCategory);
		}
	}
}
