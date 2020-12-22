﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RpsGame_NoDb;

namespace RpsGame_NoDb.Migrations
{
    [DbContext(typeof(RpsDbContext))]
    [Migration("20201222202513_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RpsGame_NoDb.Match", b =>
                {
                    b.Property<Guid>("matchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Player1playerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Player2playerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("p1RoundWins")
                        .HasColumnType("int");

                    b.Property<int>("p2RoundWins")
                        .HasColumnType("int");

                    b.Property<int>("ties")
                        .HasColumnType("int");

                    b.HasKey("matchId");

                    b.HasIndex("Player1playerId");

                    b.HasIndex("Player2playerId");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("RpsGame_NoDb.Player", b =>
                {
                    b.Property<Guid>("playerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("playerId");

                    b.ToTable("players");
                });

            modelBuilder.Entity("RpsGame_NoDb.Round", b =>
                {
                    b.Property<Guid>("roundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Player1Choice")
                        .HasColumnType("int");

                    b.Property<int>("Player2Choice")
                        .HasColumnType("int");

                    b.Property<Guid?>("WinningPlayerplayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("roundId");

                    b.HasIndex("WinningPlayerplayerId");

                    b.ToTable("rounds");
                });

            modelBuilder.Entity("RpsGame_NoDb.Match", b =>
                {
                    b.HasOne("RpsGame_NoDb.Player", "Player1")
                        .WithMany()
                        .HasForeignKey("Player1playerId");

                    b.HasOne("RpsGame_NoDb.Player", "Player2")
                        .WithMany()
                        .HasForeignKey("Player2playerId");

                    b.Navigation("Player1");

                    b.Navigation("Player2");
                });

            modelBuilder.Entity("RpsGame_NoDb.Round", b =>
                {
                    b.HasOne("RpsGame_NoDb.Player", "WinningPlayer")
                        .WithMany()
                        .HasForeignKey("WinningPlayerplayerId");

                    b.Navigation("WinningPlayer");
                });
#pragma warning restore 612, 618
        }
    }
}
