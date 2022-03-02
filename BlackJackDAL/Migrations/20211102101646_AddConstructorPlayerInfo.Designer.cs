﻿// <auto-generated />
using BlackJackDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlackJackDAL.Migrations
{
    [DbContext(typeof(BlackJackContext))]
    [Migration("20211102101646_AddConstructorPlayerInfo")]
    partial class AddConstructorPlayerInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlackJackDAL.PlayedGames", b =>
                {
                    b.Property<int>("PlayedGamesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsGameWon")
                        .HasColumnType("bit");

                    b.Property<string>("PlayerInfoNickName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PlayedGamesId");

                    b.HasIndex("PlayerInfoNickName");

                    b.ToTable("PlayedGames");
                });

            modelBuilder.Entity("BlackJackDAL.PlayerInfo", b =>
                {
                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NickName");

                    b.ToTable("PlayerInfo");
                });

            modelBuilder.Entity("BlackJackDAL.PlayedGames", b =>
                {
                    b.HasOne("BlackJackDAL.PlayerInfo", null)
                        .WithMany("PlayedGames")
                        .HasForeignKey("PlayerInfoNickName");
                });

            modelBuilder.Entity("BlackJackDAL.PlayerInfo", b =>
                {
                    b.Navigation("PlayedGames");
                });
#pragma warning restore 612, 618
        }
    }
}