﻿

using System.ComponentModel.DataAnnotations;
namespace CIS_414_Playlist_Project.Models
{
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }
        public string MoodName { get; set; } = string.Empty;

        
        public ICollection<Song> Songs { get;  set; } = new List<Song>();

        public Mood() { }

        public Mood(string moodName)
        {
            MoodName = moodName;
        }
    }
}
