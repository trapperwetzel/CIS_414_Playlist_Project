// Model for user created playlists 
// Trapper W 2/2/2025


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS_414_Playlist_Project.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required]
        public string PlaylistName { get; set; } = string.Empty;    
        public string PlaylistDescription { get; set; } = string.Empty;

        [ForeignKey("User")]
        public string UserId { get; set; }
        
        public ApplicationUser User { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();


        public Playlist() { } 

        public Playlist(string playlistName)
        {
            this.PlaylistName = playlistName;
            this.PlaylistDescription = PlaylistDescription;
            this.Songs = new List<Song>();
        }

        public void AddSongToPlaylist(Song aSong)
        {
            Songs.Add(aSong);
        }

    }
}
