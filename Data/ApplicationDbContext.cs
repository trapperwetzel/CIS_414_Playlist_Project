using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CIS_414_Playlist_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Song and Mood
            modelBuilder.Entity<Song>()
                .HasMany(s => s.Moods)
                .WithMany(m => m.Songs);

            // Seed Moods
            modelBuilder.Entity<Mood>().HasData(
                new Mood("Happy") { MoodId = 1 },
                new Mood("Sad") { MoodId = 2 },
                new Mood("Energetic") { MoodId = 3 },
                new Mood("Calm") { MoodId = 4 },
                new Mood("Romantic") { MoodId = 5 },
                new Mood("Angry") { MoodId = 6 },
                new Mood("Melancholic") { MoodId = 7 },
                new Mood("Upbeat") { MoodId = 8 }
            );
        }
    }
}

 