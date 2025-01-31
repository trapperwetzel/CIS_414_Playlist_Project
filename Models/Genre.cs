




using System.ComponentModel.DataAnnotations;
namespace CIS_414_Playlist_Project.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;

        // Navigation property for songs in this genre
        public ICollection<Song> Songs { get; set; } = new List<Song>();

        public Genre() { }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }
    }
}
