using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMagic.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _005RemappedCardsToDecklists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecklistCardsJoinTable");

            migrationBuilder.CreateTable(
                name: "DecklistCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    DecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecklistCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_DecklistCard_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decklist_DecklistCard_DecklistId",
                        column: x => x.DecklistId,
                        principalTable: "Decklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecklistCards_CardId",
                table: "DecklistCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_DecklistCards_DecklistId",
                table: "DecklistCards",
                column: "DecklistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecklistCards");

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
                name: "IX_DecklistCardsJoinTable_UsedInId",
                table: "DecklistCardsJoinTable",
                column: "UsedInId");
        }
    }
}
