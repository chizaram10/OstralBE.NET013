namespace Ostral.Domain.Models
{
    public class Student
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double GrandTotal { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }
        public ICollection<StudentCourse> CourseList { get; set; }

        public Student()
        {
            User = new User();
            CourseList = new HashSet<StudentCourse>();
        }
    }
}