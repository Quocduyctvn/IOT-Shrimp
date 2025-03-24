using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppNewConfig : IEntityTypeConfiguration<AppNews>
	{
		public void Configure(EntityTypeBuilder<AppNews> builder)
		{
			builder.ToTable("app_news");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Summary).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Path).HasMaxLength((int)MaxLength.MAX_2048);

			builder.HasOne(x => x.appCateNew)
								.WithMany(x => x.appNews)
								.HasForeignKey(x => x.IdCateNew);
		}
	}
}
