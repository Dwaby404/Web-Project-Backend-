using System.ComponentModel.DataAnnotations;

namespace AnimeApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Member"; // Default role

        // Navigation property for One-to-Many relationship
        public ICollection<Watchlist>? Watchlists { get; set; }
    }
}