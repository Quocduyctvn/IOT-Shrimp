using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtblesensorproSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Unit = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    MinThreshold = table.Column<double>(type: "double", nullable: false),
                    MaxThreshold = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_sensors", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_product_sensor",
                columns: table => new
                {
                    ProId = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_product_sensor", x => new { x.ProId, x.SensorId });
                    table.ForeignKey(
                        name: "FK_app_product_sensor_app_products_ProId",
                        column: x => x.ProId,
                        principalTable: "app_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_product_sensor_app_sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "app_sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$3n.KcjWwX0uU6pl0m3cPqeDwrncaNFAlyDVfWpYOWubHbBS.S0K8q");

            migrationBuilder.CreateIndex(
                name: "IX_app_product_sensor_SensorId",
                table: "app_product_sensor",
                column: "SensorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_product_sensor");

            migrationBuilder.DropTable(
                name: "app_sensors");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$AtSYs65/szQpCiDHGRCaV.Waxfwb5igvNvexUEDo4E171EorJ5OT2");
        }
    }
}
