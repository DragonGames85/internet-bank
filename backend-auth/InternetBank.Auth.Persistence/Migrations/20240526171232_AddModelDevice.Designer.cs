﻿// <auto-generated />
using System;
using InternetBank.Auth.Persistence.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBank.Auth.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240526171232_AddModelDevice")]
    partial class AddModelDevice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.Config", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLightTheme")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.HideAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("HideAccountUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HideAccountUserId");

                    b.ToTable("HideAccounts");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RoleUsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserRolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleUsersId", "UserRolesId");

                    b.HasIndex("UserRolesId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.Config", b =>
                {
                    b.HasOne("InternetBank.Auth.Domain.Entities.User", "User")
                        .WithOne("UserConfig")
                        .HasForeignKey("InternetBank.Auth.Domain.Entities.Config", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.Device", b =>
                {
                    b.HasOne("InternetBank.Auth.Domain.Entities.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.HideAccount", b =>
                {
                    b.HasOne("InternetBank.Auth.Domain.Entities.User", "HideAccountUser")
                        .WithMany("UserHideAccounts")
                        .HasForeignKey("HideAccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HideAccountUser");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("InternetBank.Auth.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("RoleUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetBank.Auth.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("UserRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternetBank.Auth.Domain.Entities.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("UserConfig")
                        .IsRequired();

                    b.Navigation("UserHideAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
