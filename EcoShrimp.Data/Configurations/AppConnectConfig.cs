using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppConnectConfig : IEntityTypeConfiguration<AppConnects>
	{
		public void Configure(EntityTypeBuilder<AppConnects> builder)
		{
			builder.ToTable("app_connects");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_256);

			builder.HasOne(x => x.appProInstances)
				 .WithMany(x => x.appConnects)
				 .HasForeignKey(x => x.IdProInstances);
			builder.HasOne(x => x.appSeasons)
					.WithMany(x => x.appConnects)
					.HasForeignKey(x => x.IdSeason);
		}
	}
}
