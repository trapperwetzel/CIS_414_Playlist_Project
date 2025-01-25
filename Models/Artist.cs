using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS_414_Playlist_Project.Models
{
    internal class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistId { get; private set; }
        public string ArtistName { get; private set; }
        public  ICollection<Song> Songs { get; set; }


        public Artist() : this("N/A", new List<Song>())
        {


        }
        public Artist(string aName, ICollection<Song> aCollectionOfSongs) 
        {
            this.ArtistName = aName;
            Songs = aCollectionOfSongs;



        }


    }
}
