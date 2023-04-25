using Ostral.Domain.Interfaces;

namespace Ostral.Domain.Models;

public class StudentCourse: IEntity, IAuditable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StudentId { get; set; } = string.Empty;
	public Student Student { get; set; }

    public string CourseId { get; set; } = string.Empty;
	public Course Course { get; set; }
	public double CompletionPercentage { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public StudentCourse()
    {
        Student = new Student();
        Course = new Course();
    }
}