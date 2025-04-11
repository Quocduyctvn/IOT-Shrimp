using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class udTenpnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Temp",
                table: "app_data_sensor",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.UpdateData(
                table: "app_time_intervel",
                keyColumn: "Id",
                keyValue: 1,
                column: "Label",
                value: "1 Phút");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$qQz9tM5bpeZBa62L8b5bdOYHt4TWDvBHMZ46BNe1JR/hA.LSKTttW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Temp",
                table: "app_data_sensor",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "app_time_intervel",
                keyColumn: "Id",
                keyValue: 1,
                column: "Label",
                value: "1 Giây");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$WD6ey.Jj9AXfyS36IEYQPOIYwf/yjm/ogfAJ7ZRxPK4I/HEjtgwd2");
        }
    }
}
