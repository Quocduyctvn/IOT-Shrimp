using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
	/// <inheritdoc />
	public partial class addcolumnSortOrder : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "SortOrder",
				table: "app_news",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "SortOrder",
				table: "app_cateNew",
				type: "int",
				nullable: false,
				defaultValue: 0);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "SortOrder",
				table: "app_news");

			migrationBuilder.DropColumn(
				name: "SortOrder",
				table: "app_cateNew");
		}
	}
}
