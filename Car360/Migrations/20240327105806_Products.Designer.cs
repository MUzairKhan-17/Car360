﻿// <auto-generated />
using Car360.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Car360.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240327105806_Products")]
    partial class Products
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Car360.Models.Admin", b =>
                {
                    b.Property<int>("a_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("a_id"));

                    b.Property<string>("a_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("a_mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("a_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("a_pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("a_status")
                        .HasColumnType("int");

                    b.HasKey("a_id");

                    b.ToTable("tbl_admin");
                });

            modelBuilder.Entity("Car360.Models.Product", b =>
                {
                    b.Property<int>("p_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("p_id"));

                    b.Property<string>("p_company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("p_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("p_model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("p_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("p_price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("p_status")
                        .HasColumnType("int");

                    b.HasKey("p_id");

                    b.ToTable("tbl_product");
                });

            modelBuilder.Entity("Car360.Models.Sign", b =>
                {
                    b.Property<int>("s_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("s_id"));

                    b.Property<string>("s_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("s_mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("s_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("s_pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("s_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("s_status")
                        .HasColumnType("int");

                    b.Property<string>("s_user")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("s_id");

                    b.ToTable("tbl_sign");
                });
#pragma warning restore 612, 618
        }
    }
}
