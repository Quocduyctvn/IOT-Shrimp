using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppPondConfig : IEntityTypeConfiguration<AppPonds>
	{
		public void Configure(EntityTypeBuilder<AppPonds> builder)
		{
			builder.ToTable("app_ponds");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Address).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_512);

			builder.HasOne(x => x.appFarm)
				.WithMany(x => x.appPonds)
				.HasForeignKey(x => x.IdFarm);
		}
	}
}
