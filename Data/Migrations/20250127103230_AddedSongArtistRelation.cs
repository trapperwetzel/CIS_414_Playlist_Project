using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS_414_Playlist_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSongArtistRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mood",
                table: "Mood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Song");

            migrationBuilder.RenameTable(
                name: "Song",
                newName: "Songs");

            migrationBuilder.RenameTable(
                name: "Mood",
                newName: "Moods");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Artist",
                newName: "Artists");

            migrationBuilder.RenameIndex(
                name: "IX_Song_ArtistId",
                table: "Songs",
                newName: "IX_Songs_ArtistId");

            migrationBuilder.AlterColumn<int>(
                name: "ArtistId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                column: "SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moods",
                table: "Moods",
                column: "MoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moods",
                table: "Moods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.RenameTable(
                name: "Songs",
                newName: "Song");

            migrationBuilder.RenameTable(
                name: "Moods",
                newName: "Mood");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "Artist");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistId",
                table: "Song",
                newName: "IX_Song_ArtistId");

            migrationBuilder.AlterColumn<int>(
                name: "ArtistId",
                table: "Song",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Song",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "Song",
                column: "SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mood",
                table: "Mood",
                column: "MoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistId",
                table: "Song",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId");
        }
    }
}
