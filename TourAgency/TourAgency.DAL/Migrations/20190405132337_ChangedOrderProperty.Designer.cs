﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourAgency.DAL.EF;

namespace TourAgency.DAL.Migrations
{
    [DbContext(typeof(TourAgencyContext))]
    [Migration("20190405132337_ChangedOrderProperty")]
    partial class ChangedOrderProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TourAgency.DAL.Entities.Country", b =>
                {
                    b.Property<int?>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.DailyProgram", b =>
                {
                    b.Property<int?>("DailyProgramId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Theme");

                    b.Property<int?>("TourId");

                    b.HasKey("DailyProgramId");

                    b.HasIndex("TourId");

                    b.ToTable("DailyPrograms");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Order", b =>
                {
                    b.Property<int?>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirthday");

                    b.Property<string>("DesiredHotelAccom");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsBiometricPassport");

                    b.Property<bool>("IsBookingAviaTicket");

                    b.Property<bool>("IsBookingRailwayTicket");

                    b.Property<bool>("IsInsurance");

                    b.Property<bool>("IsVisaSupport");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SerieAndNumberOfPassport");

                    b.Property<int?>("TourId");

                    b.HasKey("OrderId");

                    b.HasIndex("TourId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Picture", b =>
                {
                    b.Property<int?>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<int?>("TourId");

                    b.Property<string>("URL");

                    b.HasKey("PictureId");

                    b.HasIndex("TourId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Tour", b =>
                {
                    b.Property<int?>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int>("NumberOfDays");

                    b.Property<decimal>("Price");

                    b.Property<string>("TourName");

                    b.Property<int?>("TypeTransportTransportId");

                    b.Property<string>("Сurrency");

                    b.HasKey("TourId");

                    b.HasIndex("TypeTransportTransportId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.TourCountry", b =>
                {
                    b.Property<int?>("TourId");

                    b.Property<int?>("CountryId");

                    b.HasKey("TourId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("TourCountry");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Transport", b =>
                {
                    b.Property<int?>("TransportId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TransportName");

                    b.HasKey("TransportId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.DailyProgram", b =>
                {
                    b.HasOne("TourAgency.DAL.Entities.Tour", "Tour")
                        .WithMany("DailyPrograms")
                        .HasForeignKey("TourId");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Order", b =>
                {
                    b.HasOne("TourAgency.DAL.Entities.Tour", "Tour")
                        .WithMany("Orders")
                        .HasForeignKey("TourId");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Picture", b =>
                {
                    b.HasOne("TourAgency.DAL.Entities.Tour", "Tour")
                        .WithMany("Pictures")
                        .HasForeignKey("TourId");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.Tour", b =>
                {
                    b.HasOne("TourAgency.DAL.Entities.Transport", "TypeTransport")
                        .WithMany("Tours")
                        .HasForeignKey("TypeTransportTransportId");
                });

            modelBuilder.Entity("TourAgency.DAL.Entities.TourCountry", b =>
                {
                    b.HasOne("TourAgency.DAL.Entities.Country", "Country")
                        .WithMany("TourCountries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TourAgency.DAL.Entities.Tour", "Tour")
                        .WithMany("TourCountries")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
