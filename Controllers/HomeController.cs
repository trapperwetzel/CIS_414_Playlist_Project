using CIS_414_Playlist_Project.Data;
using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CIS_414_Playlist_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchViewModel
            {
                Results = new List<Song>(),
                // Convert Genre objects to strings
                AvailableGenres = _context.Genres.Select(g => g.GenreName).Distinct().ToList(),
                AvailableMoods = _context.Moods.Select(m => m.MoodName).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel searchModel)
        {
            var query = _context.Songs
                .Include(s => s.Genres)
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
                query = query.Where(s => s.Genres.Any(g => g.GenreName == searchModel.Genre));
            }

            if (!string.IsNullOrEmpty(searchModel.Mood))
            {
                query = query.Where(s => s.Moods.Any(m => m.MoodName == searchModel.Mood));
            }

            searchModel.Results = query.ToList();
            // Convert Genre objects to strings
            searchModel.AvailableGenres = _context.Genres.Select(g => g.GenreName).Distinct().ToList();
            searchModel.AvailableMoods = _context.Moods.Select(m => m.MoodName).ToList();

            return View("Index", searchModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
