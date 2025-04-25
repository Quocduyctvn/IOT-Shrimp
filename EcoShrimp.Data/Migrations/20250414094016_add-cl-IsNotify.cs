using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addclIsNotify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotify",
                table: "app_farms",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$88jrYvKE5lNzYSHgzZcXgew0Gkr1VLHyuru5CL9qlUQcFN.narA8e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotify",
                table: "app_farms");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$l0aoJwv7wl/T7uqrunTOSuNgrJwVH5jdc5XIDaPokYheu0Pp4DOhS");
        }
    }
}
