using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMagic.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _002AddedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorIdentities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LengthInCycles = table.Column<int>(type: "int", nullable: false),
                    GameType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorIdentityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_ColorIdentity_ColorIdentityId",
                        column: x => x.ColorIdentityId,
                        principalTable: "ColorIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CompanionId = table.Column<int>(type: "int", nullable: true),
                    DecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deck_Card_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Player_Deck_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contestants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contestants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contestant_Deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contestant_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Game_Contestant_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Decklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deck_Decklist_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecksCardsJoinTable",
                columns: table => new
                {
                    CommanderOfId = table.Column<int>(type: "int", nullable: false),
                    CommandersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecksCardsJoinTable", x => new { x.CommanderOfId, x.CommandersId });
                    table.ForeignKey(
                        name: "FK_DecksCardsJoinTable_Cards_CommandersId",
                        column: x => x.CommandersId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DecksCardsJoinTable_Decks_CommanderOfId",
                        column: x => x.CommanderOfId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecklistCardsJoinTable",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "int", nullable: false),
                    UsedInId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecklistCardsJoinTable", x => new { x.CardsId, x.UsedInId });
                    table.ForeignKey(
                        name: "FK_DecklistCardsJoinTable_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DecklistCardsJoinTable_Decklists_UsedInId",
                        column: x => x.UsedInId,
                        principalTable: "Decklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ColorIdentityId",
                table: "Cards",
                column: "ColorIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contestants_DeckId",
                table: "Contestants",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Contestants_GameId",
                table: "Contestants",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Contestants_PlayerId",
                table: "Contestants",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DecklistCardsJoinTable_UsedInId",
                table: "DecklistCardsJoinTable",
                column: "UsedInId");

            migrationBuilder.CreateIndex(
                name: "IX_Decklists_DeckId",
                table: "Decklists",
                column: "DeckId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_CompanionId",
                table: "Decks",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_OwnerId",
                table: "Decks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksCardsJoinTable_CommandersId",
                table: "DecksCardsJoinTable",
                column: "CommandersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contestants");

            migrationBuilder.DropTable(
                name: "DecklistCardsJoinTable");

            migrationBuilder.DropTable(
                name: "DecksCardsJoinTable");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Decklists");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "ColorIdentities");
        }
    }
}
