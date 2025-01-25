using CIS_414_Playlist_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CIS_414_Playlist_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Constructor with proper base constructor call
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // Public DbSet properties
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
 