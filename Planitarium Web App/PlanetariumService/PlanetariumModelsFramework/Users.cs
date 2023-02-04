using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetariumModelsFramework
{
    public class Users
    {
        public int Id { get; set; }
        //[Required]
        //[Index(IsUnique = true)]
        public string Username { get; set; } = string.Empty;
        //[Required]
        //[Index(IsUnique = true)]
        public string Email { get; set; } = string.Empty;
        //[Required]
        //[Index(IsUnique = true)]
        public string UserPassword { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
