using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress(ErrorMessage = "The email address entered is invalid. Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
        
        [Required, Phone(ErrorMessage = "Please enter a valid phone number in the format xxx-xxx-xxxx.")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required, PasswordPropertyText, RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one alphabet character, one digit character, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
