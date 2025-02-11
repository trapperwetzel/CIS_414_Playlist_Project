using System.Collections.Generic;

namespace CIS_414_Playlist_Project.Models
{
    public class PlaylistViewModel
    {
        public List<Playlist> SavedPlaylists { get; set; } = new List<Playlist>();
        public Playlist TempPlaylist { get; set; } = new Playlist();
    }
}