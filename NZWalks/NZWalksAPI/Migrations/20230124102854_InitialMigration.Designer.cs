﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalksAPI.Data;

#nullable disable

namespace NZWalksAPI.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    [Migration("20230124102854_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NZWalksAPI.Model.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Long")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Population")
                        .HasColumnType("bigint");

                    b.Property<double>("last")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("NZWalksAPI.Model.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RegionId")
                        .HasColumnType("float");

                    b.Property<Guid>("RegionId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WalkDifficultyID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RegionId1");

                    b.HasIndex("WalkDifficultyID");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalksAPI.Model.Domain.WalkDifficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WalkDifficultys");
                });

            modelBuilder.Entity("NZWalksAPI.Model.Domain.Walk", b =>
                {
                    b.HasOne("NZWalksAPI.Model.Domain.Region", "Region")
                        .WithMany("Walks")
                        .HasForeignKey("RegionId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalksAPI.Model.Domain.WalkDifficulty", "WlkDifficulty")
                        .WithMany()
                        .HasForeignKey("WalkDifficultyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");

                    b.Navigation("WlkDifficulty");
                });

            modelBuilder.Entity("NZWalksAPI.Model.Domain.Region", b =>
                {
                    b.Navigation("Walks");
                });
#pragma warning restore 612, 618
        }
    }
}
