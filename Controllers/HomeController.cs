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
                // Initialize with empty results but populated dropdowns
                Results = new List<Song>(),
                AvailableGenres = _context.Songs.Select(s => s.Genre).Distinct().ToList(),
                AvailableMoods = _context.Moods.Select(m => m.MoodName).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel searchModel)
        {
            var query = _context.Songs
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

            searchModel.Results = query.ToList();
            // Repopulate the dropdowns
            searchModel.AvailableGenres = _context.Songs.Select(s => s.Genre).Distinct().ToList();
            searchModel.AvailableMoods = _context.Moods.Select(m => m.MoodName).ToList();

            return View("Index", searchModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
