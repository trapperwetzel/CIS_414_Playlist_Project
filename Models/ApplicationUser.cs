using Microsoft.AspNetCore.Identity;

namespace CIS_414_Playlist_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

        public ApplicationUser() 
        {
            Playlists = new HashSet<Playlist>();
        }




    }
}
