using System.Text.Json;
using CIS_414_Playlist_Project.Models;

namespace CIS_414_Playlist_Project.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Artists.Any())
            {
                return;   // DB has been seeded
            }

            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "musicData.json");
            var jsonString = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var musicData = JsonSerializer.Deserialize<Dictionary<string, List<SeedData.ArtistData>>>(jsonString, options);

            if (musicData == null || !musicData.ContainsKey("artists"))
            {
                throw new InvalidDataException("Invalid JSON data structure or missing 'artists' key");
            }

            // Create HashSets to track unique genres and moods
            var uniqueGenres = new HashSet<string>();
            var uniqueMoods = new HashSet<string>();

            // First pass: collect all unique genres and moods
            foreach (var artistData in musicData["artists"])
            {
                foreach (var song in artistData.Songs)
                {
                    foreach (var genre in song.Genres)
                    {
                        uniqueGenres.Add(genre);
                    }
                    foreach (var mood in song.Moods)
                    {
                        uniqueMoods.Add(mood);
                    }
                }
            }

            // Create genres
            var genreDict = new Dictionary<string, Genre>();
            foreach (var genreName in uniqueGenres)
            {
                var genre = new Genre { GenreName = genreName };
                context.Genres.Add(genre);
                genreDict[genreName] = genre;
            }

            // Create moods
            var moodDict = new Dictionary<string, Mood>();
            foreach (var moodName in uniqueMoods)
            {
                var mood = new Mood { MoodName = moodName };
                context.Moods.Add(mood);
                moodDict[moodName] = mood;
            }

            context.SaveChanges();

            // Create artists and songs
            foreach (var artistData in musicData["artists"])
            {
                var artist = new Artist(artistData.Name);
                context.Artists.Add(artist);
                context.SaveChanges();

                foreach (var songData in artistData.Songs)
                {
                    var song = new Song(
                        artist.ArtistId,
                        artist.ArtistName,
                        songData.Title,
                        songData.DateReleased
                    );

                    // Add genres
                    foreach (var genreName in songData.Genres)
                    {
                        if (genreDict.ContainsKey(genreName))
                        {
                            song.AddGenre(genreDict[genreName]);
                        }
                    }

                    // Add moods
                    foreach (var moodName in songData.Moods)
                    {
                        if (moodDict.ContainsKey(moodName))
                        {
                            song.AddMood(moodDict[moodName]);
                        }
                    }

                    context.Songs.Add(song);
                }
            }

            context.SaveChanges();
        }
    }
}