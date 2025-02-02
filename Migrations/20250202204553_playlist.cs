using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS_414_Playlist_Project.Migrations
{
    /// <inheritdoc />
    public partial class playlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaylistDescription",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistDescription",
                table: "Playlists");
        }
    }
}
