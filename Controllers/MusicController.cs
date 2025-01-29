using CIS_414_Playlist_Project.Data;
using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS_414_Playlist_Project.Controllers
{
    public class MusicController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Search()
        {
            var viewModel = new SearchViewModel
            {
                AvailableMoods = _context.Moods.Select(m => m.MoodName).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel searchModel)
        {
            var query = _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Moods)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                query = query.Where(s =>
                    s.SongTitle.Contains(searchModel.SearchTerm) ||
                    s.ArtistName.Contains(searchModel.SearchTerm));
            }

            if (!string.IsNullOrEmpty(searchModel.Genre))
            {
                query = query.Where(s => s.Genre == searchModel.Genre);
            }

            if (!string.IsNullOrEmpty(searchModel.Mood))
            {
                query = query.Where(s => s.Moods.Any(m => m.MoodName == searchModel.Mood));
            }

            searchModel.Results = await query.ToListAsync();
            return View(searchModel);
        }
    }
}