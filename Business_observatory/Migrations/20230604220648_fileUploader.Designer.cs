﻿// <auto-generated />
using System;
using Business_observatory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Business_observatory.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230604220648_fileUploader")]
    partial class fileUploader
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Business_observatory.Models.Archivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Extension")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaSubida")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("ProyectosId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProyectosId");

                    b.ToTable("Archivos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Archivo");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Business_observatory.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Business_observatory.Models.Contacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefono")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Contactos");
                });

            modelBuilder.Entity("Business_observatory.Models.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AspNetUserId")
                        .HasColumnType("varchar(127)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Empresa")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaFinalizacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Responsable")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AspNetUserId");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("CategoriaProyecto", b =>
                {
                    b.Property<int>("CategoriasId")
                        .HasColumnType("int");

                    b.Property<int>("ProyectosId")
                        .HasColumnType("int");

                    b.HasKey("CategoriasId", "ProyectosId");

                    b.HasIndex("ProyectosId");

                    b.ToTable("CategoriaProyecto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(127)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(127)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(127)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(127)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Business_observatory.Models.FileOnDatabaseModel", b =>
                {
                    b.HasBaseType("Business_observatory.Models.Archivo");

                    b.Property<int?>("ArchivoId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasIndex("ArchivoId");

                    b.HasDiscriminator().HasValue("FileOnDatabaseModel");
                });

            modelBuilder.Entity("Business_observatory.Models.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("Business_observatory.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Business_observatory.Models.Archivo", b =>
                {
                    b.HasOne("Business_observatory.Models.Proyecto", "Proyectos")
                        .WithMany("Archivos")
                        .HasForeignKey("ProyectosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyectos");
                });

            modelBuilder.Entity("Business_observatory.Models.Proyecto", b =>
                {
                    b.HasOne("Business_observatory.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Proyectos")
                        .HasForeignKey("AspNetUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("CategoriaProyecto", b =>
                {
                    b.HasOne("Business_observatory.Models.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Business_observatory.Models.Proyecto", null)
                        .WithMany()
                        .HasForeignKey("ProyectosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Business_observatory.Models.FileOnDatabaseModel", b =>
                {
                    b.HasOne("Business_observatory.Models.Archivo", null)
                        .WithMany("FilesOnDatabase")
                        .HasForeignKey("ArchivoId");
                });

            modelBuilder.Entity("Business_observatory.Models.Archivo", b =>
                {
                    b.Navigation("FilesOnDatabase");
                });

            modelBuilder.Entity("Business_observatory.Models.Proyecto", b =>
                {
                    b.Navigation("Archivos");
                });

            modelBuilder.Entity("Business_observatory.Models.ApplicationUser", b =>
                {
                    b.Navigation("Proyectos");
                });
#pragma warning restore 612, 618
        }
    }
}
