using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppShrimpConfig : IEntityTypeConfiguration<AppShrimp>
	{
		public void Configure(EntityTypeBuilder<AppShrimp> builder)
		{
			builder.ToTable("app_shrimp");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.WebsiteName).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Logan).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.LogoUrl).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Phone).HasMaxLength((int)MaxLength.MAX_16);
			builder.Property(x => x.SubPhone).HasMaxLength((int)MaxLength.MAX_16);
			builder.Property(x => x.Email).HasMaxLength((int)MaxLength.MAX_64);
			builder.Property(x => x.Map).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Address).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.OpentTime).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.FacebookUrl).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.TwitterUrl).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.InstagramUrl).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.YouTubeUrl).HasMaxLength((int)MaxLength.MAX_512);
		}
	}
}
