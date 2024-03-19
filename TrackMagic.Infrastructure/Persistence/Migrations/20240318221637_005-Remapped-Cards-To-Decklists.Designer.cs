﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackMagic.Infrastructure.Persistence.Context;

#nullable disable

namespace TrackMagic.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240318221637_005-Remapped-Cards-To-Decklists")]
    partial class _005RemappedCardsToDecklists
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DecksCardsJoinTable", b =>
                {
                    b.Property<int>("CommanderOfId")
                        .HasColumnType("int");

                    b.Property<int>("CommandersId")
                        .HasColumnType("int");

                    b.HasKey("CommanderOfId", "CommandersId");

                    b.HasIndex("CommandersId");

                    b.ToTable("DecksCardsJoinTable");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CardTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColorIdentityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ColorIdentityId");

                    b.ToTable("Cards", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.ColorIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Colors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ColorIdentities", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Contestant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Contestants", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanionId")
                        .HasColumnType("int");

                    b.Property<int?>("DecklistId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanionId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Decks", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Decklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId")
                        .IsUnique()
                        .HasFilter("[DeckId] IS NOT NULL");

                    b.ToTable("Decklists", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.DecklistCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("DecklistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("DecklistId");

                    b.ToTable("DecklistCards", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<int>("LengthInCycles")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games", (string)null);
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("DecksCardsJoinTable", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.Deck", null)
                        .WithMany()
                        .HasForeignKey("CommanderOfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackMagic.Domain.Entities.Card", null)
                        .WithMany()
                        .HasForeignKey("CommandersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Card", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.ColorIdentity", "ColorIdentity")
                        .WithMany("CardsInIdentity")
                        .HasForeignKey("ColorIdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Card_ColorIdentity_ColorIdentityId");

                    b.Navigation("ColorIdentity");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Contestant", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.Deck", "Deck")
                        .WithMany("PilotedBy")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Contestant_Deck_DeckId");

                    b.HasOne("TrackMagic.Domain.Entities.Game", "Game")
                        .WithMany("Contestants")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Game_Contestant_GameId");

                    b.HasOne("TrackMagic.Domain.Entities.Player", "Player")
                        .WithMany("Contested")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Contestant_Player_PlayerId");

                    b.Navigation("Deck");

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Deck", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.Card", "Companion")
                        .WithMany("CompanionOf")
                        .HasForeignKey("CompanionId")
                        .HasConstraintName("FK_Deck_Card_CompanionId");

                    b.HasOne("TrackMagic.Domain.Entities.Player", "Owner")
                        .WithMany("Decks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Player_Deck_OwnerId");

                    b.Navigation("Companion");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Decklist", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.Deck", "Deck")
                        .WithOne("Decklist")
                        .HasForeignKey("TrackMagic.Domain.Entities.Decklist", "DeckId")
                        .HasConstraintName("FK_Deck_Decklist_DeckId");

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.DecklistCard", b =>
                {
                    b.HasOne("TrackMagic.Domain.Entities.Card", "Card")
                        .WithMany("Usage")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Card_DecklistCard_CardId");

                    b.HasOne("TrackMagic.Domain.Entities.Decklist", "Decklist")
                        .WithMany("Cards")
                        .HasForeignKey("DecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Decklist_DecklistCard_DecklistId");

                    b.Navigation("Card");

                    b.Navigation("Decklist");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Card", b =>
                {
                    b.Navigation("CompanionOf");

                    b.Navigation("Usage");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.ColorIdentity", b =>
                {
                    b.Navigation("CardsInIdentity");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Deck", b =>
                {
                    b.Navigation("Decklist");

                    b.Navigation("PilotedBy");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Decklist", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Game", b =>
                {
                    b.Navigation("Contestants");
                });

            modelBuilder.Entity("TrackMagic.Domain.Entities.Player", b =>
                {
                    b.Navigation("Contested");

                    b.Navigation("Decks");
                });
#pragma warning restore 612, 618
        }
    }
}
