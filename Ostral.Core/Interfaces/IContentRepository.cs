using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
	public interface IContentRepository
	{
		Task<IEnumerable<Content>> GetAllCourseContentById(string courseId);
		Task<Content> GetContentById(string contentId);
	}
}
