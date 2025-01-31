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

        // Helper method to create sample data
        public static Dictionary<string, List<ArtistData>> GetSampleData()
        {
            return new Dictionary<string, List<ArtistData>>
            {
                {
                    "artists", new List<ArtistData>
                    {
                        new ArtistData
                        {
                            Name = "The Beatles",
                            Songs = new List<SongData>
                            {
                                new SongData
                                {
                                    Title = "Hey Jude",
                                    DateReleased = "1968-08-26",
                                    Genres = new List<string> { "Rock", "Pop" },
                                    Moods = new List<string> { "Uplifting", "Mellow" }
                                },
                                new SongData
                                {
                                    Title = "Let It Be",
                                    DateReleased = "1970-03-06",
                                    Genres = new List<string> { "Rock", "Gospel" },
                                    Moods = new List<string> { "Peaceful", "Inspirational" }
                                }
                            }
                        },
                        new ArtistData
                        {
                            Name = "Queen",
                            Songs = new List<SongData>
                            {
                                new SongData
                                {
                                    Title = "Bohemian Rhapsody",
                                    DateReleased = "1975-10-31",
                                    Genres = new List<string> { "Rock", "Opera" },
                                    Moods = new List<string> { "Epic", "Dramatic" }
                                }
                            }
                        }
                        // Add more artists as needed
                    }
                }
            };
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