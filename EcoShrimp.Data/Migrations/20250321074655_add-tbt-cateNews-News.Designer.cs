﻿// <auto-generated />
using System;
using EcoShrimp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoShrimp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250321074655_add-tbt-cateNews-News")]
    partial class addtbtcateNewsNews
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppCateNews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("app_cateNew", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("app_categories", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppConnects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdProInstances")
                        .HasColumnType("int");

                    b.Property<int>("IdSeason")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdProInstances");

                    b.HasIndex("IdSeason");

                    b.ToTable("app_connects", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppDataSensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("DO")
                        .HasColumnType("double");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdConnect")
                        .HasColumnType("int");

                    b.Property<double?>("Nh4")
                        .HasColumnType("double");

                    b.Property<double?>("PH")
                        .HasColumnType("double");

                    b.Property<double?>("Sal")
                        .HasColumnType("double");

                    b.Property<double>("Temp")
                        .HasColumnType("double");

                    b.Property<double?>("Tur")
                        .HasColumnType("double");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdConnect");

                    b.ToTable("app_data_sensor", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppFarms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("City")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("District")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Email")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Location")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("NumberHouse")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Ward")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("app_farms", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppImges", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.ToTable("app_imges", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppNews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdCateNew")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("varchar(2048)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdCateNew");

                    b.ToTable("app_news", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppPonds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<decimal?>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("IdFarm")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdFarm");

                    b.ToTable("app_ponds", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProInstances", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("BuyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("IP")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("IdFarm")
                        .HasColumnType("int");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int?>("Port")
                        .HasColumnType("int");

                    b.Property<string>("SeriNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdFarm");

                    b.HasIndex("IdProduct");

                    b.ToTable("app_product_Instances", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<double>("OriginalPrice")
                        .HasColumnType("double");

                    b.Property<double>("SalePrice")
                        .HasColumnType("double");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Video")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.ToTable("app_products", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppRequests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("ContentFeedback")
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(96)
                        .HasColumnType("varchar(96)");

                    b.Property<DateTime?>("FeedbackDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Phone")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TitleFeedback")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("app_requests", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppSeasons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Density")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasMaxLength(2048)
                        .HasColumnType("varchar(2048)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdPond")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdPond");

                    b.ToTable("app_seasons", (string)null);
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppConnects", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppProInstances", "appProInstances")
                        .WithMany("appConnects")
                        .HasForeignKey("IdProInstances")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoShrimp.Data.Entities.AppSeasons", "appSeasons")
                        .WithMany("appConnects")
                        .HasForeignKey("IdSeason")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appProInstances");

                    b.Navigation("appSeasons");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppDataSensor", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppConnects", "appConnect")
                        .WithMany("appDataSensors")
                        .HasForeignKey("IdConnect")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appConnect");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppImges", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppProducts", "appProduct")
                        .WithMany("appImges")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appProduct");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppNews", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppCateNews", "appCateNew")
                        .WithMany("appNews")
                        .HasForeignKey("IdCateNew")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appCateNew");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppPonds", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppFarms", "appFarm")
                        .WithMany("appPonds")
                        .HasForeignKey("IdFarm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appFarm");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProInstances", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppFarms", "appFarm")
                        .WithMany("appProInstances")
                        .HasForeignKey("IdFarm");

                    b.HasOne("EcoShrimp.Data.Entities.AppProducts", "appProducts")
                        .WithMany("appProInstances")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appFarm");

                    b.Navigation("appProducts");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProducts", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppCategories", "appCategory")
                        .WithMany("appProducts")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appCategory");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppSeasons", b =>
                {
                    b.HasOne("EcoShrimp.Data.Entities.AppPonds", "appPond")
                        .WithMany("appSeasons")
                        .HasForeignKey("IdPond")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appPond");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppCateNews", b =>
                {
                    b.Navigation("appNews");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppCategories", b =>
                {
                    b.Navigation("appProducts");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppConnects", b =>
                {
                    b.Navigation("appDataSensors");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppFarms", b =>
                {
                    b.Navigation("appPonds");

                    b.Navigation("appProInstances");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppPonds", b =>
                {
                    b.Navigation("appSeasons");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProInstances", b =>
                {
                    b.Navigation("appConnects");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppProducts", b =>
                {
                    b.Navigation("appImges");

                    b.Navigation("appProInstances");
                });

            modelBuilder.Entity("EcoShrimp.Data.Entities.AppSeasons", b =>
                {
                    b.Navigation("appConnects");
                });
#pragma warning restore 612, 618
        }
    }
}
