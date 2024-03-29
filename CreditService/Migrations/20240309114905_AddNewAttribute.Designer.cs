﻿// <auto-generated />
using System;
using CreditService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CreditService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240309114905_AddNewAttribute")]
    partial class AddNewAttribute
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CreditService.Model.Entity.CreditTariff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("MaxCreditSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MaxRepaymentPeriod")
                        .HasColumnType("int");

                    b.Property<decimal?>("MinCreditSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MinRepaymentPeriod")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<decimal>("PennyPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("rateType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CreditTariff");
                });

            modelBuilder.Entity("CreditService.Model.Entity.UserCreditEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Currency")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentPeriod")
                        .HasColumnType("int");

                    b.Property<int>("RepaymentPeriod")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.ToTable("Credit");
                });

            modelBuilder.Entity("CreditService.Model.Entity.UserCreditEntity", b =>
                {
                    b.HasOne("CreditService.Model.Entity.CreditTariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tariff");
                });
#pragma warning restore 612, 618
        }
    }
}
