using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS_414_Playlist_Project.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        public string SongTitle { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        [Required]
        public string ArtistName { get; set; }

        [Required]
        public string DateReleased { get; set; }



        // Navigation properties
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Mood> Moods { get; set; }
        
        public Song()
        {
            SongTitle = "N/A";
            ArtistName = "N/A";
            DateReleased = "??/??/???";
            Genres = new List<Genre>();
            Moods = new List<Mood>();
        }

        public Song(string songTitle, string artistName, string dateReleased)
        {
            SongTitle = songTitle;
            ArtistName = artistName;
            DateReleased = dateReleased;
            Genres = new List<Genre>();
            Moods = new List<Mood>();
        }

        public Song(int artistId, string artistName, string songTitle, string dateReleased)
            : this(songTitle, artistName, dateReleased)
        {
            ArtistId = artistId;
        }

        public void AddGenre(Genre genre)
        {
            Genres ??= new List<Genre>();
            if (!Genres.Contains(genre))
            {
                Genres.Add(genre);
            }
        }

        public void AddMood(Mood mood)
        {
            Moods ??= new List<Mood>();
            if (!Moods.Contains(mood))
            {
                Moods.Add(mood);
            }
        }
    }
}