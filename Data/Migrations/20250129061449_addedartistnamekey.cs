using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS_414_Playlist_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedartistnamekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MoodSong",
                columns: table => new
                {
                    MoodsMoodId = table.Column<int>(type: "int", nullable: false),
                    SongsSongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodSong", x => new { x.MoodsMoodId, x.SongsSongId });
                    table.ForeignKey(
                        name: "FK_MoodSong_Moods_MoodsMoodId",
                        column: x => x.MoodsMoodId,
                        principalTable: "Moods",
                        principalColumn: "MoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoodSong_Songs_SongsSongId",
                        column: x => x.SongsSongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoodSong_SongsSongId",
                table: "MoodSong",
                column: "SongsSongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoodSong");

            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Songs");
        }
    }
}
