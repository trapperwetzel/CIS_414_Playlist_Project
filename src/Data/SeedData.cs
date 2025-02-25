namespace CIS_414_Playlist_Project.Data
{
    public static class SeedData
    {
        public class ArtistData
        {
            public string Name { get; set; }
            public List<SongData> Songs { get; set; }

            // Add constructor to initialize Songs
            public ArtistData()
            {
                Songs = new List<SongData>();
            }
        }
        public class SongData
        {
            public string Title { get; set; }
            public string DateReleased { get; set; }
            // Changed from single Genre to List of Genres
            public List<string> Genres { get; set; }
            public List<string> Moods { get; set; }

            // Constructor to ensure lists are initialized
            public SongData()
            {
                Genres = new List<string>();
                Moods = new List<string>();
            }
        }

        
        

        // Validation method to ensure data integrity
        public static bool ValidateData(Dictionary<string, List<ArtistData>> data)
        {
            if (!data.ContainsKey("artists")) return false;

            foreach (var artist in data["artists"])
            {
                if (string.IsNullOrWhiteSpace(artist.Name)) return false;
                if (artist.Songs == null || !artist.Songs.Any()) return false;

                foreach (var song in artist.Songs)
                {
                    if (string.IsNullOrWhiteSpace(song.Title)) return false;
                    if (string.IsNullOrWhiteSpace(song.DateReleased)) return false;
                    if (song.Genres == null || !song.Genres.Any()) return false;
                    if (song.Moods == null || !song.Moods.Any()) return false;

                    // Validate date format (YYYY-MM-DD)
                    if (!DateTime.TryParse(song.DateReleased, out _)) return false;
                }
            }

            return true;
        }
    }
}