using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CIS_414_Playlist_Project.Models
{
    public class Genre
    {

        [Key]
        public int GenreId { get;  set; }
        public string GenreName { get;  set; } = String.Empty;
        


    }
}
