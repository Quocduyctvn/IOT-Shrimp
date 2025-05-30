﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class dlthresolth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_threshold");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$ZVmh5mJ3AP6/QdNtzeGryu7bwfeXgedIiy5Q9QtEHJbkjcHlCcY1C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_threshold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdFarm = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Desc = table.Column<string>(type: "longtext", nullable: true),
                    Max = table.Column<double>(type: "double", nullable: false),
                    Min = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false),
                    Unit = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_threshold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_threshold_app_farms_IdFarm",
                        column: x => x.IdFarm,
                        principalTable: "app_farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "app_users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pass",
                value: "$2a$10$jNT0xxBr4zJ6sdql6f6eNOZfwA0k4dxQ4FLN2LrB5lXVByap9.Bym");

            migrationBuilder.CreateIndex(
                name: "IX_app_threshold_IdFarm",
                table: "app_threshold",
                column: "IdFarm");
        }
    }
}
