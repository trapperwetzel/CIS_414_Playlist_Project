using CIS_414_Playlist_Project.Data;

namespace CIS_414_Playlist_Project.Models
{
    public class PlaylistService : IPlaylistService
    {

        private readonly ApplicationDbContext _context;

        public PlaylistService(ApplicationDbContext context)
        {
            _context = context;
        }









        


        public void PrintPlaylist()
        {
            throw new System.NotImplementedException();
        }
        public void  GetUserPlaylists()
        {
            throw new System.NotImplementedException();
        }
    }
}
