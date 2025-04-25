using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class dltbleEnviIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_prod_env_idx");

            migrationBuilder.DropTable(
                name: "app_env_idx");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$l0aoJwv7wl/T7uqrunTOSuNgrJwVH5jdc5XIDaPokYheu0Pp4DOhS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_env_idx",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    MaxThreshold = table.Column<double>(type: "double", nullable: false),
                    MinThreshold = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_env_idx", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_prod_env_idx",
                columns: table => new
                {
                    ProId = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_prod_env_idx", x => new { x.ProId, x.SensorId });
                    table.ForeignKey(
                        name: "FK_app_prod_env_idx_app_env_idx_SensorId",
                        column: x => x.SensorId,
                        principalTable: "app_env_idx",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_prod_env_idx_app_products_ProId",
                        column: x => x.ProId,
                        principalTable: "app_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$RHPsLg.5lEQkjKw8FJnbSOCrmnmxSqZUpmnMD.yAn0L2W2xntMsFK");

            migrationBuilder.CreateIndex(
                name: "IX_app_prod_env_idx_SensorId",
                table: "app_prod_env_idx",
                column: "SensorId");
        }
    }
}
