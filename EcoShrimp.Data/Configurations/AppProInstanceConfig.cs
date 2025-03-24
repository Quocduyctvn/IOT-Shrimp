using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppProInstanceConfig : IEntityTypeConfiguration<AppProInstances>
	{
		public void Configure(EntityTypeBuilder<AppProInstances> builder)
		{
			builder.ToTable("app_product_Instances");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.IP).HasMaxLength((int)MaxLength.MAX_32);

			builder.HasOne(x => x.appFarm)
				.WithMany(x => x.appProInstances)
				.HasForeignKey(x => x.IdFarm);
			builder.HasOne(x => x.appProducts)
				.WithMany(x => x.appProInstances)
				.HasForeignKey(x => x.IdProduct);
		}
	}
}
