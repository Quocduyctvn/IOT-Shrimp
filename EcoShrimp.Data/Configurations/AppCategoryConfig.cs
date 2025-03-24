using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppCategoryConfig : IEntityTypeConfiguration<AppCategories>
	{
		public void Configure(EntityTypeBuilder<AppCategories> builder)
		{
			builder.ToTable("app_categories");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_256);
			builder.Property(x => x.Desc).HasMaxLength((int)MaxLength.MAX_512);
		}
	}
}
