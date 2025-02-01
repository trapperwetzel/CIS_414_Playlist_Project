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

        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Song-Genre many-to-many relationship
            modelBuilder.Entity<Song>()
                .HasMany(s => s.Genres)
                .WithMany(g => g.Songs)
                .UsingEntity(j => j.ToTable("SongGenres"));

            // Configure Song-Mood many-to-many relationship
            modelBuilder.Entity<Song>()
                .HasMany(s => s.Moods)
                .WithMany(m => m.Songs)
                .UsingEntity(j => j.ToTable("SongMoods"));

            // Configure Song-Artist relationship
            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId);

            // Configure Playlist-Song relationship
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Songs);
                
                
            // Seed Moods
            modelBuilder.Entity<Mood>().HasData(
                new Mood("Happy") { MoodId = 1 },
                new Mood("Sad") { MoodId = 2 },
                new Mood("Energetic") { MoodId = 3 },
                new Mood("Calm") { MoodId = 4 },
                new Mood("Romantic") { MoodId = 5 },
                new Mood("Angry") { MoodId = 6 }
                
            );
        }
    }
}

 