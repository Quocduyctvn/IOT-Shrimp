using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class udcolumnAppPoliciessorted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "app_policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "app_policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$AtSYs65/szQpCiDHGRCaV.Waxfwb5igvNvexUEDo4E171EorJ5OT2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "app_policies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "app_policies");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$UztMrBTzbwcXw/c3NJiRyOl/AkHVbj/td.2hb.6FcfPTqCTB5Ooyy");
        }
    }
}
