using Ostral.Domain.Models;

namespace Ostral.Core.DTOs
{
	public class CategoryDTO
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public IEnumerable<CourseDTO> CourseList { get; set; }

		public CategoryDTO()
		{
			CourseList =  new HashSet<CourseDTO>();
		}
	}
}
