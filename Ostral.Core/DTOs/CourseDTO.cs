namespace Ostral.Core.DTOs
{
    public class CourseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Duration { get; set; }
        public string TutorFullName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int ContentCount { get; set; }
    }
}
