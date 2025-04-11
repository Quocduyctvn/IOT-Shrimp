using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppUserConfig : IEntityTypeConfiguration<AppUsers>
	{
		public void Configure(EntityTypeBuilder<AppUsers> builder)
		{
			builder.ToTable("app_users");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Email).HasMaxLength((int)MaxLength.MAX_64);
			builder.Property(x => x.Phone).HasMaxLength((int)MaxLength.MAX_64);
			builder.Property(x => x.Address).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Avatar).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Pass).HasMaxLength((int)MaxLength.MAX_512);

			builder.HasOne(x => x.appRole).WithMany(x => x.appUsers)
					.HasForeignKey(x => x.IdRole);
		}
	}
}
