using System.ComponentModel.DataAnnotations;

namespace PlanetariumModels
{
    public class Users
    {
        public int Id { get; set; }
        // [Required]
        public string Username { get; set; } = string.Empty;
        // [Required]
        public string UserPassword { get; set; } = string.Empty;
        // [Required]
        public string Email { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
