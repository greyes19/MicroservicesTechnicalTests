﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Products.Infraestructure;

#nullable disable

namespace Products.Infraestructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Products.Domain.Services.Models.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(28,15)");

                    b.Property<DateTime>("CreateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LotNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(28,15)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
