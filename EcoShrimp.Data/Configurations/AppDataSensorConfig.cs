using EcoShrimp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoShrimp.Data.Configurations
{
	public class AppDataSensorConfig : IEntityTypeConfiguration<AppDataSensor>
	{
		public void Configure(EntityTypeBuilder<AppDataSensor> builder)
		{
			builder.ToTable("app_data_sensor");
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.appConnect)
				.WithMany(x => x.appDataSensors)
				.HasForeignKey(x => x.IdConnect);
		}
	}
}
