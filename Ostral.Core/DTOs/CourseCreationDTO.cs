using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
    public class CourseCreationDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required, MaxLength(150)]
        public string Description { get; set; } = string.Empty;

    }
}
