using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS_414_Playlist_Project.Models
{
    public class Song
    {
        [Key]
        public int SongId { get;  set; }

        [ForeignKey("ArtistID")]
        public int ArtistId { get;  set; }

        [ForeignKey("ArtistName")]
        public string ArtistName { get;  set; } = "N/A";

        [ForeignKey("Genre")]
        public string Genre { get;  set; } = "N/A";

        public Artist Artist { get;  set; }
        public Genre SongGenre { get;  set; }

        public string SongTitle { get;  set; } = "N/A";
        public string DateReleased { get;  set; } = "??/??/???";

        // Many-to-many relationship with Mood
        public virtual ICollection<Mood> Moods { get;  set; } = new List<Mood>();

        public Song()
        {
        }

        public Song(int artistId, string songTitle, string dateReleased)
        {
            ArtistId = artistId;
            SongTitle = songTitle;
            DateReleased = dateReleased;
        }

        // Method to add a mood to the song
        public void AddMood(Mood mood)
        {
            if (!Moods.Contains(mood))
            {
                Moods.Add(mood);
            }
        }
    }
}