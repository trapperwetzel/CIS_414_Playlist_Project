using CIS_414_Playlist_Project.Data;
using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// 1/31/2025 Trapper W

namespace CIS_414_Playlist_Project.Controllers
{
    public class MusicController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public IActionResult FilterByGenre(string genre)
        {
            var songs = _context.Songs
                .Include(s => s.Genres)
                .Include(s => s.Moods)
                .Where(s => s.Genres.Any(g => g.GenreName.ToLower() == genre.ToLower()))
                .ToList();

            return View("Index", songs);
        }


    }
}