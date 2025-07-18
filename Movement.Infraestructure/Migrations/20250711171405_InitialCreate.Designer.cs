﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movement.Infraestructure;

#nullable disable

namespace Movement.Infraestructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250711171405_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movement.Domain.Services.Models.MovementDetailModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovementHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("MovementHeaderId");

                    b.ToTable("MovementDetails", (string)null);
                });

            modelBuilder.Entity("Movement.Domain.Services.Models.MovementHeaderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovementType")
                        .HasColumnType("int");

                    b.Property<Guid>("OriginDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("MovementHeaders", (string)null);
                });

            modelBuilder.Entity("Movement.Domain.Services.Models.MovementDetailModel", b =>
                {
                    b.HasOne("Movement.Domain.Services.Models.MovementHeaderModel", "MovementHeader")
                        .WithMany("MovementDetails")
                        .HasForeignKey("MovementHeaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MovementHeader");
                });

            modelBuilder.Entity("Movement.Domain.Services.Models.MovementHeaderModel", b =>
                {
                    b.Navigation("MovementDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
