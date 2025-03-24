using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppSeasonConfig : IEntityTypeConfiguration<AppSeasons>
	{
		public void Configure(EntityTypeBuilder<AppSeasons> builder)
		{
			builder.ToTable("app_seasons");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_2048);

			builder.HasOne(x => x.appPond)
				.WithMany(x => x.appSeasons)
				.HasForeignKey(x => x.IdPond);
		}
	}
}
