namespace CIS_414_Playlist_Project.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; } = string.Empty;    

        public ICollection<Song> Songs { get; set; } = new List<Song>();


        public Playlist() { } 

        public Playlist(string playlistName)
        {
            this.PlaylistName = playlistName; 
        }

    }
}
