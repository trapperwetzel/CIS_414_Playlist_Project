using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIS_414_Playlist_Project.Data;
using CIS_414_Playlist_Project.Models;
using System.Text;

namespace CIS_414_Playlist_Project.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaylistController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SavePlaylist(string playlistName, List<int> songIds)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistName))
                {
                    return Json(new { success = false, message = "Playlist name is required." });
                }
                if (songIds == null || !songIds.Any())
                {
                    return Json(new { success = false, message = "No songs selected." });
                }

                // Retrieve the songs from the database
                var songs = await _context.Songs.Where(s => songIds.Contains(s.SongId)).ToListAsync();

                // Create a new playlist and add the songs
                var playlist = new Playlist(playlistName);
                foreach (var song in songs)
                {
                    playlist.AddSongToPlaylist(song);
                }

                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();

                // Return the playlist ID so client-side can store it for further edits.
                return Json(new { success = true, playlistId = playlist.PlaylistId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(int playlistId, int songId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId);
            if (playlist == null)
                return Json(new { success = false, message = "Playlist not found." });

            if (playlist.Songs.Any(s => s.SongId == songId))
                return Json(new { success = false, message = "Song already exists in the playlist." });

            var song = await _context.Songs.FirstOrDefaultAsync(s => s.SongId == songId);
            if (song == null)
                return Json(new { success = false, message = "Song not found." });

            playlist.Songs.Add(song);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSong(int playlistId, int songId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId);
            if (playlist == null)
                return Json(new { success = false, message = "Playlist not found." });

            var song = playlist.Songs.FirstOrDefault(s => s.SongId == songId);
            if (song == null)
                return Json(new { success = false, message = "Song not in the playlist." });

            playlist.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ExportPlaylist(int playlistId)
        {
            // Retrieve the playlist including its songs
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId);

            if (playlist == null)
            {
                return NotFound();
            }

            // Build CSV content
            var csv = new StringBuilder();
            csv.AppendLine("SongId,SongTitle,ArtistName");

            foreach (var song in playlist.Songs)
            {
                // Escape commas if needed
                var songTitle = song.SongTitle.Contains(",") ? $"\"{song.SongTitle}\"" : song.SongTitle;
                var artistName = song.ArtistName.Contains(",") ? $"\"{song.ArtistName}\"" : song.ArtistName;
                csv.AppendLine($"{song.SongId},{songTitle},{artistName}");
            }

            // Convert CSV string to byte array
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());

            // Return file with a filename based on the playlist name
            return File(buffer, "text/csv", $"{playlist.PlaylistName}.csv");
        }
    }
}
