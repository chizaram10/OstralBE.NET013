using Ostral.Domain.Models;

namespace Ostral.Core.DTOs
{
    public class CourseDetailedDTO : CourseDTO
    {
        public string TutorId { get; set; } = string.Empty;
        public string TutorImageUrl { get; set; } = string.Empty;
        public string TutorDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Completed { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
		public int StudentCount { get; set; }
	}
}
