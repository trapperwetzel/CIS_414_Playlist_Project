using CIS_414_Playlist_Project.Data;
using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CIS_414_Playlist_Project.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionPlaylistKey = "TempPlaylist";

        public PlaylistController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Main Playlist Page
        public async Task<IActionResult> PlaylistPage()
        {
            var viewModel = new PlaylistViewModel
            {
                SavedPlaylists = new List<Playlist>(),
                TempPlaylist = GetTempPlaylist()
            };

            if (User.Identity.IsAuthenticated)
            {
                viewModel.SavedPlaylists = await _context.Playlists
                    .Where(p => p.UserId == User.Identity.Name)
                    .Include(p => p.Songs)
                    .ToListAsync();
            }

            return View(viewModel);
        }

        // Helper method to get temporary playlist from session
        private Playlist GetTempPlaylist()
        {
            var tempPlaylistJson = HttpContext.Session.GetString(SessionPlaylistKey);
            if (string.IsNullOrEmpty(tempPlaylistJson))
            {
                var newPlaylist = new Playlist { PlaylistName = "Current Playlist" };
                HttpContext.Session.SetString(SessionPlaylistKey, JsonSerializer.Serialize(newPlaylist));
                return newPlaylist;
            }
            return JsonSerializer.Deserialize<Playlist>(tempPlaylistJson);
        }

        private void SaveTempPlaylist(Playlist playlist)
        {
            HttpContext.Session.SetString(SessionPlaylistKey, JsonSerializer.Serialize(playlist));
        }

        // Add song to temporary playlist
        [HttpPost]
        public IActionResult AddToPlaylist(int songId)
        {
            var tempPlaylist = GetTempPlaylist();
            var song = _context.Songs.Find(songId);

            if (song != null && !tempPlaylist.Songs.Any(s => s.SongId == songId))
            {
                tempPlaylist.Songs.Add(song);
                SaveTempPlaylist(tempPlaylist);
            }

            return RedirectToAction(nameof(PlaylistPage));
        }

        // Remove song from temporary playlist
        [HttpPost]
        public IActionResult RemoveFromPlaylist(int songId)
        {
            var tempPlaylist = GetTempPlaylist();
            var song = tempPlaylist.Songs.FirstOrDefault(s => s.SongId == songId);

            if (song != null)
            {
                tempPlaylist.Songs.Remove(song);
                SaveTempPlaylist(tempPlaylist);
            }

            return RedirectToAction(nameof(PlaylistPage));
        }

        // Clear temporary playlist
        [HttpPost]
        public IActionResult ClearPlaylist()
        {
            var tempPlaylist = GetTempPlaylist();
            tempPlaylist.Songs.Clear();
            SaveTempPlaylist(tempPlaylist);
            return RedirectToAction(nameof(PlaylistPage));
        }

        // Save temporary playlist as permanent (for logged-in users)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SavePlaylist(string playlistName)
        {
            var tempPlaylist = GetTempPlaylist();

            var newPlaylist = new Playlist
            {
                PlaylistName = playlistName,
                PlaylistDescription = "Created on " + DateTime.UtcNow.ToString("yyyy-MM-dd"),
                UserId = User.Identity.Name,
                Songs = new List<Song>(tempPlaylist.Songs)
            };

            _context.Playlists.Add(newPlaylist);
            await _context.SaveChangesAsync();

            // Clear temp playlist after saving
            tempPlaylist.Songs.Clear();
            SaveTempPlaylist(tempPlaylist);

            return RedirectToAction(nameof(PlaylistPage));
        }

        // View saved playlist details
        public async Task<IActionResult> PlaylistDetails(int? id)
        {
            if (id == null)
                return NotFound();

            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);

            if (playlist == null)
                return NotFound();

            return View(playlist);
        }

        // Delete saved playlist
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(p => p.PlaylistId == id && p.UserId == User.Identity.Name);

            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(PlaylistPage));
        }
    }
}
