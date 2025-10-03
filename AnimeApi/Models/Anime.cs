using System.ComponentModel.DataAnnotations;

namespace AnimeApi.Models
{
    public class Anime
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [MaxLength(50)]
        public string Code { get; set; } // Unique identifier like "AOT-2013"

        [MaxLength(1000)]
        public string Synopsis { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Episodes must be between 1 and 10000")]
        public int Episodes { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Release year must be valid")]
        public int ReleaseYear { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        public decimal Rating { get; set; }

        // Navigation property
        public ICollection<Watchlist>? Watchlists { get; set; }
    }
}