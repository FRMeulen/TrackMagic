using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMagic.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _006ChangedDeckDecklist_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_Decklist_DeckId",
                table: "Decklists");

            migrationBuilder.DropIndex(
                name: "IX_Decklists_DeckId",
                table: "Decklists");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_DecklistId",
                table: "Decks",
                column: "DecklistId",
                unique: true,
                filter: "[DecklistId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_Decklist_DecklistId",
                table: "Decks",
                column: "DecklistId",
                principalTable: "Decklists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_Decklist_DecklistId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_DecklistId",
                table: "Decks");

            migrationBuilder.CreateIndex(
                name: "IX_Decklists_DeckId",
                table: "Decklists",
                column: "DeckId",
                unique: true,
                filter: "[DeckId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_Decklist_DeckId",
                table: "Decklists",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id");
        }
    }
}
