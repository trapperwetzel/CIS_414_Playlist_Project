using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS_414_Playlist_Project.Models

{
    internal class Song
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongId { get; }

        private static Artist SongArtist = new Artist();
        public string ArtistName { get; } = SongArtist.ArtistName;
        public string SongTitle { get; } = "N/A";
        public string DateReleased { get; } = "??/??/???";

        
        public Song()
        {

        }
        public Song(string aArtistName,int aSongID, string aSongTitle, string aDateReleased)
        {
            this.ArtistName = aArtistName;
            this.SongId = aSongID;
            this.SongTitle = aSongTitle;
            this.DateReleased = aDateReleased;
        }
    }
}
