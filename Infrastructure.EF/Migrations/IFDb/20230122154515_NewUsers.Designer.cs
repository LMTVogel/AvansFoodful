﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.EF.Migrations.IFDb
{
    [DbContext(typeof(IFDbContext))]
    [Migration("20230122154515_NewUsers")]
    partial class NewUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8dbf2938-3dbe-472d-8ee9-3596f95e7931",
                            ConcurrencyStamp = "22b64c74-4728-481b-9dfb-e55e5240bd3f",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529",
                            ConcurrencyStamp = "e4c3514f-ca6d-48a1-a36f-f34337a66837",
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "002942bd-9cac-44b4-896e-bbab0a96189f",
                            Email = "lmt.vogel@student.avans.nl",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "LMT.VOGEL@STUDENT.AVANS.NL",
                            NormalizedUserName = "LMT.VOGEL@STUDENT.AVANS.NL",
                            PasswordHash = "AQAAAAEAACcQAAAAEKo9YqKkuAy8HCMa2tst7vdbuTKdfomDXCH2cpoSZRqAsdP4SPSdrO7wuGy/QgYM2A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "736f0c69-9611-4a30-92c9-8d2fd78105e9",
                            TwoFactorEnabled = false,
                            UserName = "lmt.vogel@student.avans.nl"
                        },
                        new
                        {
                            Id = "7b445865-a24d-4543-a6c6-9443d048cd1c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9c0a2815-2101-4c3b-be37-151b0a62e7bc",
                            Email = "rm.vandergaag@student.avans.nl",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "RM.VANDERGAAG@STUDENT.AVANS.NL",
                            NormalizedUserName = "RM.VANDERGAAG@STUDENT.AVANS.NL",
                            PasswordHash = "AQAAAAEAACcQAAAAEO1usYIl09ojjX5yOELJt3ai+vpnGbdwYTpwpq6ZmEyQPCSb2z/EZHzh6bUH5AYDuQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "be3b1a1f-1637-4306-a427-cc03ca1db55d",
                            TwoFactorEnabled = false,
                            UserName = "rm.vandergaag@student.avans.nl"
                        },
                        new
                        {
                            Id = "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ec345916-df25-421b-a844-6dba2116547b",
                            Email = "arend@avans.nl",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "AREND@AVANS.NL",
                            NormalizedUserName = "AREND@AVANS.NL",
                            PasswordHash = "AQAAAAEAACcQAAAAEK1R1hoGoruTI9A30HMVoKlfW3Tio7XwKj0ThBspfE4zY54rCIVyyzlVvmfHxy0/PA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "165dd393-81f9-4eca-81be-cb76fade3378",
                            TwoFactorEnabled = false,
                            UserName = "arend@avans.nl"
                        },
                        new
                        {
                            Id = "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2ea78c09-8ac6-469d-afd5-6c831876b71b",
                            Email = "gerard@avans.nl",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GERARD@AVANS.NL",
                            NormalizedUserName = "GERARD@AVANS.NL",
                            PasswordHash = "AQAAAAEAACcQAAAAEOcv51N2I+52S5Dvkm89eK7RAM5vwXLRg+PjGldVa89yDbtR3LWiJOi2BgtdoNNiqQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8d53dec4-7e49-42f5-8c68-5f7d749d8766",
                            TwoFactorEnabled = false,
                            UserName = "gerard@avans.nl"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            RoleId = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529"
                        },
                        new
                        {
                            UserId = "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae",
                            RoleId = "8dbf2938-3dbe-472d-8ee9-3596f95e7931"
                        },
                        new
                        {
                            UserId = "7b445865-a24d-4543-a6c6-9443d048cd1c",
                            RoleId = "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529"
                        },
                        new
                        {
                            UserId = "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd",
                            RoleId = "8dbf2938-3dbe-472d-8ee9-3596f95e7931"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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
#pragma warning restore 612, 618
        }
    }
}
