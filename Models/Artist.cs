
using System.ComponentModel.DataAnnotations;


namespace CIS_414_Playlist_Project.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get;  set; }
        public string ArtistName { get;  set; }

        
        public  ICollection<Song> Songs { get;  set; }


        public Artist()
        {
            // Initialize collections to avoid null references
            Songs = new List<Song>();
        }

        public Artist(string aName)
        {
            ArtistName = aName;
            Songs = new List<Song>();
        }
    }
}
