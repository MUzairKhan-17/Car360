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
    [Migration("20240323093448_Signs")]
    partial class Signs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
