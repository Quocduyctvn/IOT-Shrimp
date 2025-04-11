using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.DataSeeders
{
	public class AppShrimpSeeder : IEntityTypeConfiguration<AppShrimp>
	{
		public void Configure(EntityTypeBuilder<AppShrimp> builder)
		{
			builder.HasData(
				new AppShrimp
				{
					Id = 1,
					WebsiteName = "EcoShrimp.com",
					Logan = "EcoShrimp, Công nghệ IoT – Đồng hành cùng người nuôi tôm trong kỷ nguyên số!",
					LogoUrl = "/assets/images/logo/logo-EcoShrimp-text.png",

					// Thông tin liên hệ
					Phone = "090 100 7221",
					SubPhone = "034 888 6431",
					Email = "quocduyctvn@gmail.com",
					Address = "Hẻm 388, P An Khánh, Q Ninh Kiều, TPCT",
					Map = "",
					OpentTime = "T2 - T7: 7h30 đến 16h00",

					// Mạng xã hội
					FacebookUrl = "https://facebook.com/ecoshrimp",
					TwitterUrl = "https://twitter.com/ecoshrimp",
					InstagramUrl = "https://instagram.com/ecoshrimp",
					YouTubeUrl = "https://youtube.com/ecoshrimp"
				}
			);
		}
	}
}
