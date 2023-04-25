using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models
{
    public class Tutor : IEntity, IAuditable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double TotalRevenue { get; set; }
        public ICollection<Course> CourseList { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public User User { get; set; }


        public Tutor()
        {
            User = new User();
            CourseList = new HashSet<Course>();
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}