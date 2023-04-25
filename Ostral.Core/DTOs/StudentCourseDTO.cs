namespace Ostral.Core.DTOs;

public class StudentCourseDTO
{
    public string Id { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public double CompletionPercentage { get; set; }
    public DateTime? CompletionDate { get; set; }
}