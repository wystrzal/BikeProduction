﻿// <auto-generated />
using System;
using Delivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Delivery.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Delivery.Core.Models.LoadingPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountOfSpace")
                        .HasColumnType("int");

                    b.Property<int>("LoadedQuantity")
                        .HasColumnType("int");

                    b.Property<int>("LoadingPlaceNumber")
                        .HasColumnType("int");

                    b.Property<int>("LoadingPlaceStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LoadingPlaces");
                });

            modelBuilder.Entity("Delivery.Core.Models.PackToDelivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LoadingPlaceId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PackStatus")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductsQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoadingPlaceId");

                    b.ToTable("PacksToDelivery");
                });

            modelBuilder.Entity("Delivery.Core.Models.PackToDelivery", b =>
                {
                    b.HasOne("Delivery.Core.Models.LoadingPlace", "LoadingPlace")
                        .WithMany("PacksToDelivery")
                        .HasForeignKey("LoadingPlaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
