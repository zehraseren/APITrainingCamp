﻿// <auto-generated />
using System;
using ApiProjectCamp.WebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiProjectCamp.WebApi.Migrations;

[DbContext(typeof(ApiContext))]
[Migration("20250309082742_update_entities_category_and_product")]
partial class update_entities_category_and_product
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.12")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("ApiProjectCamp.Entities.Category", b =>
            {
                b.Property<int>("CategoryId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                b.Property<string>("CategoryName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("CategoryId");

                b.ToTable("Categories");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Chef", b =>
            {
                b.Property<int>("ChefId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChefId"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("NameSurname")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ChefId");

                b.ToTable("Chefs");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Contact", b =>
            {
                b.Property<int>("ContactId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("MapLocation")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OpenHours")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ContactId");

                b.ToTable("Contacts");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Feature", b =>
            {
                b.Property<int>("FeatureId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeatureId"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Subtitle")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("VideoUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("FeatureId");

                b.ToTable("Features");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Image", b =>
            {
                b.Property<int>("ImageId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ImageId");

                b.ToTable("Images");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Message", b =>
            {
                b.Property<int>("MessageId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsRead")
                    .HasColumnType("bit");

                b.Property<string>("MessageDetails")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("NameSurname")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("SendDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Subject")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("MessageId");

                b.ToTable("Messages");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Product", b =>
            {
                b.Property<int>("ProductId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                b.Property<int?>("CategoryId")
                    .HasColumnType("int");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("ProductDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ProductName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ProductId");

                b.HasIndex("CategoryId");

                b.ToTable("Products");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Reservation", b =>
            {
                b.Property<int>("ReservationId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Message")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("NameSurname")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("PersonCount")
                    .HasColumnType("int");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateOnly>("ReservationDate")
                    .HasColumnType("date");

                b.Property<TimeOnly>("ReservationHour")
                    .HasColumnType("time");

                b.Property<string>("ReservationStatus")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ReservationId");

                b.ToTable("Reservations");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Service", b =>
            {
                b.Property<int>("ServiceId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("IconUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ServiceId");

                b.ToTable("Services");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Testimonial", b =>
            {
                b.Property<int>("TestimonialId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestimonialId"));

                b.Property<string>("Comment")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("NameSurname")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("TestimonialId");

                b.ToTable("Testimonials");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Product", b =>
            {
                b.HasOne("ApiProjectCamp.Entities.Category", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId");

                b.Navigation("Category");
            });

        modelBuilder.Entity("ApiProjectCamp.Entities.Category", b =>
            {
                b.Navigation("Products");
            });
#pragma warning restore 612, 618
    }
}
