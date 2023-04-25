using Ostral.Domain.Enums;
using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models
{
    public class Category : IEntity, IAuditable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ICollection<Course> CourseList { get; set; }

        public Category() => CourseList = new HashSet<Course>();
    }
}