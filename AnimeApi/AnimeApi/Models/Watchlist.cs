using System.ComponentModel.DataAnnotations;

namespace AnimeApi.Models
{
    public class Watchlist
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AnimeId { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        // Navigation properties for relationships
        public User User { get; set; }
        public Anime Anime { get; set; }
    }
}