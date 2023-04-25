using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
