using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppImgeConfig : IEntityTypeConfiguration<AppImges>
	{
		public void Configure(EntityTypeBuilder<AppImges> builder)
		{
			builder.ToTable("app_imges");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Path).HasMaxLength((int)MaxLength.MAX_512);

			builder.HasOne(x => x.appProduct)
				.WithMany(x => x.appImges)
				.HasForeignKey(x => x.IdProduct);
		}
	}
}
