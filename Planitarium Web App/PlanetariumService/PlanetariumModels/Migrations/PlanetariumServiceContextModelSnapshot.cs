﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanetariumModels;

#nullable disable

namespace PlanetariumModels.Migrations
{
    [DbContext(typeof(PlanetariumServiceContext))]
    partial class PlanetariumServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlanetariumModels.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte>("Capacity")
                        .HasColumnType("tinyint");

                    b.Property<string>("HallName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Halls", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("EventDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Performances", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Poster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnType("datetime2");

                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("PerformanceId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("PerformanceId");

                    b.ToTable("Posters", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<byte>("Place")
                        .HasColumnType("tinyint");

                    b.Property<int>("PosterId")
                        .HasColumnType("int");

                    b.Property<string>("TicketStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PosterId");

                    b.HasIndex("TierId");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Tier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Surcharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tiers", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("PlanetariumModels.Poster", b =>
                {
                    b.HasOne("PlanetariumModels.Hall", "Hall")
                        .WithMany("Posters")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanetariumModels.Performance", "Performance")
                        .WithMany("Posters")
                        .HasForeignKey("PerformanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("PlanetariumModels.Ticket", b =>
                {
                    b.HasOne("PlanetariumModels.Orders", "Order")
                        .WithMany("Tickets")
                        .HasForeignKey("OrderId");

                    b.HasOne("PlanetariumModels.Poster", "Poster")
                        .WithMany("Tickets")
                        .HasForeignKey("PosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanetariumModels.Tier", "Tier")
                        .WithMany("Tickets")
                        .HasForeignKey("TierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Poster");

                    b.Navigation("Tier");
                });

            modelBuilder.Entity("PlanetariumModels.Hall", b =>
                {
                    b.Navigation("Posters");
                });

            modelBuilder.Entity("PlanetariumModels.Orders", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("PlanetariumModels.Performance", b =>
                {
                    b.Navigation("Posters");
                });

            modelBuilder.Entity("PlanetariumModels.Poster", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("PlanetariumModels.Tier", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
