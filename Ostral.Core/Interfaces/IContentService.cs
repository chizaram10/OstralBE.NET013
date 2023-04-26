using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
	public interface IContentService
	{
		Task<Result<IEnumerable<ContentDTO>>> GetAllCourseContentById(string courseId);
		Task<Result<ContentDetailedDTO>> GetContentById(string contentId);
		Task<Result<ContentDTO>> CreateContent(ContentCreationDTO data, string courseId);
	}
}
