using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CIS_414_Playlist_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoodRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SongGenreGenreId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Moods",
                columns: new[] { "MoodId", "MoodName" },
                values: new object[,]
                {
                    { 1, "Happy" },
                    { 2, "Sad" },
                    { 3, "Energetic" },
                    { 4, "Calm" },
                    { 5, "Romantic" },
                    { 6, "Angry" },
                    { 7, "Melancholic" },
                    { 8, "Upbeat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongGenreGenreId",
                table: "Songs",
                column: "SongGenreGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_SongGenreGenreId",
                table: "Songs",
                column: "SongGenreGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_SongGenreGenreId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_SongGenreGenreId",
                table: "Songs");

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Moods",
                keyColumn: "MoodId",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongGenreGenreId",
                table: "Songs");
        }
    }
}
