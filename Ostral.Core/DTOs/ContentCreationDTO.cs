using Microsoft.AspNetCore.Http;
using Ostral.Domain.Enums;
using Ostral.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
    public class ContentCreationDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public double Duration { get; set; }

        [Required, DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
