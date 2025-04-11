using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.DataSeeders
{
	public class AppTimeIntervalSeeder : IEntityTypeConfiguration<AppTimeIntervals>
	{
		public void Configure(EntityTypeBuilder<AppTimeIntervals> builder)
		{
			builder.HasData(
				new AppTimeIntervals { Id = 1, Value = 1, Label = "1 Phút" },
				new AppTimeIntervals { Id = 2, Value = 5, Label = "5 Phút" },
				new AppTimeIntervals { Id = 3, Value = 10, Label = "10 Phút" },
				new AppTimeIntervals { Id = 4, Value = 30, Label = "30 Phút" },
				new AppTimeIntervals { Id = 5, Value = 60, Label = "1 Tiếng" }
			);
		}
	}
}
