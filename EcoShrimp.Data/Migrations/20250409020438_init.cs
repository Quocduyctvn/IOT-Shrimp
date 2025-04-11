using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoShrimp.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
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
                    table.PrimaryKey("PK_app_categories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_cateNew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "longtext", nullable: true),
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
                    table.PrimaryKey("PK_app_cateNew", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    FarmName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    OwnerName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    NumberHouse = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    Ward = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    District = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    City = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    Location = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Avatar = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PassWord = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdTime = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_farms", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Table = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    GroupName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_permissions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "varchar(96)", maxLength: 96, nullable: false),
                    Content = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: false),
                    ContentFeedback = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: true),
                    TitleFeedback = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_requests", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_shrimp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WebsiteName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Logan = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    LogoUrl = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    SubPhone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Map = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    OpentTime = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    FacebookUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    TwitterUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    InstagramUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    YouTubeUrl = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_shrimp", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_time_intervel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_time_intervel", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    OriginalPrice = table.Column<double>(type: "double", nullable: false),
                    SalePrice = table.Column<double>(type: "double", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Video = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Summary = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Desc = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_products_app_categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "app_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Path = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false),
                    Summary = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdCateNew = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_news", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_news_app_cateNew_IdCateNew",
                        column: x => x.IdCateNew,
                        principalTable: "app_cateNew",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_ponds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdFarm = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_ponds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_ponds_app_farms_IdFarm",
                        column: x => x.IdFarm,
                        principalTable: "app_farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_role_per",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    IdPermission = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_role_per", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_role_per_app_permissions_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "app_permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_role_per_app_roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "app_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 256, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Phone = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Pass = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    Avatar = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_users_app_roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "app_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_imges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_imges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_imges_app_products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "app_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_product_Instances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeriNumber = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Port = table.Column<int>(type: "int", nullable: true),
                    BuyDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdFarm = table.Column<int>(type: "int", nullable: true),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_product_Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_product_Instances_app_farms_IdFarm",
                        column: x => x.IdFarm,
                        principalTable: "app_farms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_app_product_Instances_app_products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "app_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Desc = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Density = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IdPond = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_seasons_app_ponds_IdPond",
                        column: x => x.IdPond,
                        principalTable: "app_ponds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_connects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Desc = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdProInstances = table.Column<int>(type: "int", nullable: false),
                    IdSeason = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_connects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_connects_app_product_Instances_IdProInstances",
                        column: x => x.IdProInstances,
                        principalTable: "app_product_Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_connects_app_seasons_IdSeason",
                        column: x => x.IdSeason,
                        principalTable: "app_seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "app_data_sensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PH = table.Column<double>(type: "double", nullable: true),
                    Temp = table.Column<double>(type: "double", nullable: false),
                    DO = table.Column<double>(type: "double", nullable: true),
                    Nh4 = table.Column<double>(type: "double", nullable: true),
                    Sal = table.Column<double>(type: "double", nullable: true),
                    Tur = table.Column<double>(type: "double", nullable: true),
                    Tds = table.Column<double>(type: "double", nullable: true),
                    IdConnect = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_data_sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_data_sensor_app_connects_IdConnect",
                        column: x => x.IdConnect,
                        principalTable: "app_connects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "app_permissions",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DeletedDate", "Desc", "GroupName", "Table", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách danh mục", "Quản lý danh mục", "AppCategories", null, null },
                    { 1102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm danh mục", "Quản lý danh mục", "AppCategories", null, null },
                    { 1103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật danh mục", "Quản lý danh mục", "AppCategories", null, null },
                    { 1104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa danh mục", "Quản lý danh mục", "AppCategories", null, null },
                    { 1105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết danh mục", "Quản lý danh mục", "AppCategories", null, null },
                    { 2101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách danh mục tin tức", "Quản lý danh mục tin tức", "AppCateNews", null, null },
                    { 2102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm danh mục tin tức", "Quản lý danh mục tin tức", "AppCateNews", null, null },
                    { 2103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật danh mục tin tức", "Quản lý danh mục tin tức", "AppCateNews", null, null },
                    { 2104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa danh mục tin tức", "Quản lý danh mục tin tức", "AppCateNews", null, null },
                    { 2105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết danh mục tin tức", "Quản lý danh mục tin tức", "AppCateNews", null, null },
                    { 3101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách trang trại", "Quản lý trang trại", "AppFarms", null, null },
                    { 3102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm trang trại", "Quản lý trang trại", "AppFarms", null, null },
                    { 3103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật trang trại", "Quản lý trang trại", "AppFarms", null, null },
                    { 3104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa trang trại", "Quản lý trang trại", "AppFarms", null, null },
                    { 3105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết trang trại", "Quản lý trang trại", "AppFarms", null, null },
                    { 4101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách tin tức", "Quản lý tin tức", "AppNews", null, null },
                    { 4102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm tin tức", "Quản lý tin tức", "AppNews", null, null },
                    { 4103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật tin tức", "Quản lý tin tức", "AppNews", null, null },
                    { 4104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa tin tức", "Quản lý tin tức", "AppNews", null, null },
                    { 4105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết tin tức", "Quản lý tin tức", "AppNews", null, null },
                    { 5101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách sản phẩm", "Quản lý sản phẩm", "AppProducts", null, null },
                    { 5102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm sản phẩm", "Quản lý sản phẩm", "AppProducts", null, null },
                    { 5103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật sản phẩm", "Quản lý sản phẩm", "AppProducts", null, null },
                    { 5104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa sản phẩm", "Quản lý sản phẩm", "AppProducts", null, null },
                    { 5105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết sản phẩm", "Quản lý sản phẩm", "AppProducts", null, null },
                    { 6101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách yêu cầu", "Quản lý yêu cầu", "AppRequests", null, null },
                    { 6102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm yêu cầu", "Quản lý yêu cầu", "AppRequests", null, null },
                    { 6103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật yêu cầu", "Quản lý yêu cầu", "AppRequests", null, null },
                    { 6104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa yêu cầu", "Quản lý yêu cầu", "AppRequests", null, null },
                    { 6105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết yêu cầu", "Quản lý yêu cầu", "AppRequests", null, null },
                    { 7101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách vai trò", "Quản lý vai trò", "AppRoles", null, null },
                    { 7102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm vai trò", "Quản lý vai trò", "AppRoles", null, null },
                    { 7103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật vai trò", "Quản lý vai trò", "AppRoles", null, null },
                    { 7104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa vai trò", "Quản lý vai trò", "AppRoles", null, null },
                    { 7105, "VIEW_DETAIL", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem chi tiết vai trò", "Quản lý vai trò", "AppRoles", null, null },
                    { 8101, "VIEW_LIST", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xem danh sách người dùng", "Quản lý người dùng", "AppUsers", null, null },
                    { 8102, "CREATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thêm người dùng", "Quản lý người dùng", "AppUsers", null, null },
                    { 8103, "UPDATE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cập nhật người dùng", "Quản lý người dùng", "AppUsers", null, null },
                    { 8104, "DELETE", null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xóa người dùng", "Quản lý người dùng", "AppUsers", null, null }
                });

            migrationBuilder.InsertData(
                table: "app_roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "Desc", "Name", "SortOrder", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quản trị toàn bộ hệ thống", "Quản trị hệ thống", null, null, null });

            migrationBuilder.InsertData(
                table: "app_shrimp",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "DeletedDate", "Email", "FacebookUrl", "InstagramUrl", "Logan", "LogoUrl", "Map", "OpentTime", "Phone", "SubPhone", "TwitterUrl", "UpdatedBy", "UpdatedDate", "WebsiteName", "YouTubeUrl" },
                values: new object[] { 1, "Hẻm 388, P An Khánh, Q Ninh Kiều, TPCT", null, null, null, "quocduyctvn@gmail.com", "https://facebook.com/ecoshrimp", "https://instagram.com/ecoshrimp", "EcoShrimp, Công nghệ IoT – Đồng hành cùng người nuôi tôm trong kỷ nguyên số!", "/assets/images/logo/logo-EcoShrimp-text.png", "", "T2 - T7: 7h30 đến 16h00", "090 100 7221", "034 888 6431", "https://twitter.com/ecoshrimp", null, null, "EcoShrimp.com", "https://youtube.com/ecoshrimp" });

            migrationBuilder.InsertData(
                table: "app_time_intervel",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "Label", "UpdatedBy", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, null, null, null, "1 Giây", null, null, 1 },
                    { 2, null, null, null, "5 Phút", null, null, 5 },
                    { 3, null, null, null, "10 Phút", null, null, 10 },
                    { 4, null, null, null, "30 Phút", null, null, 30 },
                    { 5, null, null, null, "1 Tiếng", null, null, 60 }
                });

            migrationBuilder.InsertData(
                table: "app_users",
                columns: new[] { "Id", "Address", "Avatar", "CreatedBy", "CreatedDate", "DeletedDate", "Email", "IdRole", "Name", "Pass", "Phone", "SortOrder", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "132, Hưng Lợi, Ninh Kiều, Cần Thơ", null, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "quocduyctvn@gmail.com", 1, "Quốc Duy", "$2a$10$WD6ey.Jj9AXfyS36IEYQPOIYwf/yjm/ogfAJ7ZRxPK4I/HEjtgwd2", "0901007221", null, 1, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_app_connects_IdProInstances",
                table: "app_connects",
                column: "IdProInstances");

            migrationBuilder.CreateIndex(
                name: "IX_app_connects_IdSeason",
                table: "app_connects",
                column: "IdSeason");

            migrationBuilder.CreateIndex(
                name: "IX_app_data_sensor_IdConnect",
                table: "app_data_sensor",
                column: "IdConnect");

            migrationBuilder.CreateIndex(
                name: "IX_app_imges_IdProduct",
                table: "app_imges",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_app_news_IdCateNew",
                table: "app_news",
                column: "IdCateNew");

            migrationBuilder.CreateIndex(
                name: "IX_app_ponds_IdFarm",
                table: "app_ponds",
                column: "IdFarm");

            migrationBuilder.CreateIndex(
                name: "IX_app_product_Instances_IdFarm",
                table: "app_product_Instances",
                column: "IdFarm");

            migrationBuilder.CreateIndex(
                name: "IX_app_product_Instances_IdProduct",
                table: "app_product_Instances",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_app_products_IdCategory",
                table: "app_products",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_app_role_per_IdPermission",
                table: "app_role_per",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_app_role_per_IdRole",
                table: "app_role_per",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_app_seasons_IdPond",
                table: "app_seasons",
                column: "IdPond");

            migrationBuilder.CreateIndex(
                name: "IX_app_users_IdRole",
                table: "app_users",
                column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_data_sensor");

            migrationBuilder.DropTable(
                name: "app_imges");

            migrationBuilder.DropTable(
                name: "app_news");

            migrationBuilder.DropTable(
                name: "app_requests");

            migrationBuilder.DropTable(
                name: "app_role_per");

            migrationBuilder.DropTable(
                name: "app_shrimp");

            migrationBuilder.DropTable(
                name: "app_time_intervel");

            migrationBuilder.DropTable(
                name: "app_users");

            migrationBuilder.DropTable(
                name: "app_connects");

            migrationBuilder.DropTable(
                name: "app_cateNew");

            migrationBuilder.DropTable(
                name: "app_permissions");

            migrationBuilder.DropTable(
                name: "app_roles");

            migrationBuilder.DropTable(
                name: "app_product_Instances");

            migrationBuilder.DropTable(
                name: "app_seasons");

            migrationBuilder.DropTable(
                name: "app_products");

            migrationBuilder.DropTable(
                name: "app_ponds");

            migrationBuilder.DropTable(
                name: "app_categories");

            migrationBuilder.DropTable(
                name: "app_farms");
        }
    }
}
