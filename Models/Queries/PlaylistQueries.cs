using CIS_414_Playlist_Project.Data;

namespace CIS_414_Playlist_Project.Models.Queries
{
    public class PlaylistQueries
    {
        private readonly ApplicationDbContext _context;

        public PlaylistQueries(ApplicationDbContext context)
        {
            _context = context;
        }



    }
}
