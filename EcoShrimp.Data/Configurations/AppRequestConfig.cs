using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppRequestConfig : IEntityTypeConfiguration<AppRequests>
	{
		public void Configure(EntityTypeBuilder<AppRequests> builder)
		{
			builder.ToTable("app_requests");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.CompanyName).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Address).HasMaxLength((int)MaxLength.MAX_512);
			builder.Property(x => x.Phone).HasMaxLength((int)MaxLength.MAX_16);
			builder.Property(x => x.Email).HasMaxLength((int)MaxLength.MAX_96);
			builder.Property(x => x.Content).HasMaxLength((int)MaxLength.MAX_4096);
			builder.Property(x => x.ContentFeedback).HasMaxLength((int)MaxLength.MAX_4096);
			builder.Property(x => x.TitleFeedback).HasMaxLength((int)MaxLength.MAX_1024);
		}
	}
}
