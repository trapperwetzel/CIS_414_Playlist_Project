namespace CIS_414_Playlist_Project.Models
{
    public interface IPlaylistService
    {
        
        void PrintPlaylist(); // Returns all the songs on a users playlist (especially useful for when someone doesn't have an account. 
        void GetUserPlaylists(); // Returns all the playlists a user has. 

        


    }
}
