using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.DataSeeders
{
	public class AppUsersSeeder : IEntityTypeConfiguration<AppUsers>
	{
		public void Configure(EntityTypeBuilder<AppUsers> builder)
		{
			var now = new DateTime(2024, 3, 10);

			var defaultPassword = "Quocduyctvn423*";
			var passHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

			builder.HasData(
				new AppUsers
				{
					Id = 1,
					Name = "Quốc Duy",
					Address = "132, Hưng Lợi, Ninh Kiều, Cần Thơ",
					Email = "quocduyctvn@gmail.com",
					Phone = "0901007221",
					Pass = passHash,
					Avatar = null,
					Status = Status.Active,
					IdRole = 1,
					CreatedDate = now
				}
			);
		}
	}
}
