namespace CIS_414_Playlist_Project.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string Genre { get; set; }
        public string Mood { get; set; }
        public List<Song> Results { get; set; } = new List<Song>();
        public List<string> AvailableMoods { get; set; } = new List<string>();
        public List<string> AvailableGenres { get; set; } = new List<string>();
        public Playlist CurrentPlaylist { get; set; } = new Playlist();
    }
}