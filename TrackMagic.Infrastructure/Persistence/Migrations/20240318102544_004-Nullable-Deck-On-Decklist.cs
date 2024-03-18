using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMagic.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _004NullableDeckOnDecklist : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "DeckId",
                table: "Decklists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_Decklist_DeckId",
                table: "Decklists");

            migrationBuilder.DropIndex(
                name: "IX_Decklists_DeckId",
                table: "Decklists");

            migrationBuilder.AlterColumn<int>(
                name: "DeckId",
                table: "Decklists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decklists_DeckId",
                table: "Decklists",
                column: "DeckId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_Decklist_DeckId",
                table: "Decklists",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
