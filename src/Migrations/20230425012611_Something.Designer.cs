﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Prometheus.Data;

#nullable disable

namespace Prometheus.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230425012611_Something")]
    partial class Something
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Prometheus.Data.Entities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Announcements", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TeamId");

                    b.ToTable("Children", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("FirstTeamId")
                        .HasColumnType("integer");

                    b.Property<int>("FirstTeamScore")
                        .HasColumnType("integer");

                    b.Property<int>("SecondTeamId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondTeamScore")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FirstTeamId")
                        .IsUnique();

                    b.HasIndex("SecondTeamId")
                        .IsUnique();

                    b.HasIndex("TeamId");

                    b.ToTable("Matches", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasVoucher")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Paid")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("PaidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Token");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7300),
                            Deleted = false,
                            Slug = "administrator",
                            UpdatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7300)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310),
                            Deleted = false,
                            Slug = "teacher",
                            UpdatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310),
                            Deleted = false,
                            Slug = "parent",
                            UpdatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310)
                        });
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("Prometheus.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("873a9236-0e62-412c-b738-c2da6413cd2d"),
                            CreatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7400),
                            Deleted = false,
                            Email = "admin@saei.com",
                            LastName = "Administrator",
                            Name = "Administrator",
                            Password = "$2a$13$E39SWmc3yZf7xJVfCJZ38.frnEoRg87xx0lNAsMDgl2JPa5i5cLYW",
                            Phone = "1234567890",
                            RoleId = 1,
                            UpdatedAt = new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7410)
                        });
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Announcement", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Child", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.User", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prometheus.Data.Entities.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId");

                    b.Navigation("Parent");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Match", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.Team", "FirstTeam")
                        .WithOne()
                        .HasForeignKey("Prometheus.Data.Entities.Match", "FirstTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prometheus.Data.Entities.Team", "SecondTeam")
                        .WithOne()
                        .HasForeignKey("Prometheus.Data.Entities.Match", "SecondTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prometheus.Data.Entities.Team", null)
                        .WithMany("Matches")
                        .HasForeignKey("TeamId");

                    b.Navigation("FirstTeam");

                    b.Navigation("SecondTeam");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Payment", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.RefreshToken", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Team", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.User", "Trainer")
                        .WithMany("Teams")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.User", b =>
                {
                    b.HasOne("Prometheus.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.Team", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Prometheus.Data.Entities.User", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Payments");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
