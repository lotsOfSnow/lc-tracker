﻿// <auto-generated />
using System;
using LcTracker.Core.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LcTracker.Core.Storage.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240520180314_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LcTracker.Core.Features.AppUsers.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("018eb88f-3667-7787-9ff4-6024332b04b9")
                        });
                });

            modelBuilder.Entity("LcTracker.Core.Features.Attempts.Attempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<bool>("HasSolved")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasUsedHelp")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRecap")
                        .HasColumnType("boolean");

                    b.Property<int?>("MinutesSpent")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasMaxLength(50000)
                        .HasColumnType("character varying(50000)");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("LcTracker.Core.Features.Problems.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Slug")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("LcTracker.Core.Features.Attempts.Attempt", b =>
                {
                    b.HasOne("LcTracker.Core.Features.AppUsers.AppUser", null)
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LcTracker.Core.Features.Problems.Problem", null)
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LcTracker.Core.Features.Problems.Problem", b =>
                {
                    b.HasOne("LcTracker.Core.Features.AppUsers.AppUser", null)
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("LcTracker.Core.Features.Problems.ProblemMethod", "Methods", b1 =>
                        {
                            b1.Property<string>("Name")
                                .HasMaxLength(40)
                                .HasColumnType("character varying(40)");

                            b1.Property<string>("Contents")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("character varying(5000)");

                            b1.Property<Guid>("ProblemId")
                                .HasColumnType("uuid");

                            b1.HasKey("Name");

                            b1.HasIndex("ProblemId");

                            b1.ToTable("ProblemMethod");

                            b1.WithOwner()
                                .HasForeignKey("ProblemId");
                        });

                    b.Navigation("Methods");
                });
#pragma warning restore 612, 618
        }
    }
}
