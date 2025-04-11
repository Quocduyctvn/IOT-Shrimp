using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppTimeIntervalConfig : IEntityTypeConfiguration<AppTimeIntervals>
	{
		public void Configure(EntityTypeBuilder<AppTimeIntervals> builder)
		{
			builder.ToTable("app_time_intervel");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Label).HasMaxLength((int)MaxLength.MAX_128);
		}
	}
}
