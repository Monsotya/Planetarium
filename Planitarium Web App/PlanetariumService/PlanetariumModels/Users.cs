using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetariumModels
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? UserRole { get; set; } = string.Empty;
    }
}
