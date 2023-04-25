using Microsoft.AspNetCore.Identity;
using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models
{
    public class User : IdentityUser, IAuditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? PublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}