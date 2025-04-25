using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppPolicyConfig : IEntityTypeConfiguration<AppPolicies>
	{
		public void Configure(EntityTypeBuilder<AppPolicies> builder)
		{
			builder.ToTable("app_policies");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Path).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Summary).HasMaxLength((int)MaxLength.MAX_1024);
			builder.Property(x => x.Content).HasMaxLength((int)MaxLength.MAX_262144);
		}
	}
}
