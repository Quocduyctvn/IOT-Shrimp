using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppFarmConfig : IEntityTypeConfiguration<AppFarms>
	{
		public void Configure(EntityTypeBuilder<AppFarms> builder)
		{
			builder.ToTable("app_farms");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength((int)MaxLength.MAX_32);
			builder.Property(x => x.FarmName).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.OwnerName).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.NumberHouse).HasMaxLength((int)MaxLength.MAX_16);
			builder.Property(x => x.Ward).HasMaxLength((int)MaxLength.MAX_128);
			builder.Property(x => x.District).HasMaxLength((int)MaxLength.MAX_128);
			builder.Property(x => x.City).HasMaxLength((int)MaxLength.MAX_128);
			builder.Property(x => x.Location).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Phone).HasMaxLength((int)MaxLength.MAX_16);
			builder.Property(x => x.Email).HasMaxLength((int)MaxLength.MAX_32);
			builder.Property(x => x.Avatar).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.PassWord).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_512);

		}
	}
}
