using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS_414_Playlist_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveArtistForeignKey1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Artist_ArtistId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_ArtistId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_ArtistId",
                table: "Genre",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Artist_ArtistId",
                table: "Genre",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId");
        }
    }
}
