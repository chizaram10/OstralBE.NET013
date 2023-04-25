using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required, Phone(ErrorMessage = "Please enter a valid phone number in the format xxx-xxx-xxxx.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, EmailAddress(ErrorMessage = "The email address entered is invalid. Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}
