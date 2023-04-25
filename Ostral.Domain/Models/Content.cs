using Ostral.Domain.Enums;
using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models
{
    public class Content : IEntity, IAuditable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public ContentMediaType Type { get; set; }
        public double Duration { get; set; }
        public bool IsDownloadable { get; set; }
        public decimal Percentage { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public DateTime? Completed { get; set; }

        public string CourseId { get; set; } = string.Empty;
        public Course Course { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Content()
        {
            Course = new Course();
        }
    }
}