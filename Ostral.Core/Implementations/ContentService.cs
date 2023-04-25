using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _courseContentRepository;

        public ContentService(IContentRepository courseContentRepository)
        {
            _courseContentRepository = courseContentRepository;
        }

        public async Task<Result<IEnumerable<ContentDTO>>> GetAllCourseContentById(string courseId)
        {
            try
            {
                var courseContent = await _courseContentRepository.GetAllCourseContentById(courseId);

                if (courseContent.Any())
                    return new Result<IEnumerable<ContentDTO>>
                    {
                        Success = false,
                        Errors = new string[] { $"No content found for course '{courseId}'." }
                    };

                return new Result<IEnumerable<ContentDTO>>
                {
                    Success = true,
                    Data = courseContent.Select(c => CreateCourseDTO(c))
                };
            }
            catch (Exception ex) { return new Result<IEnumerable<ContentDTO>>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                }; 
            }
        }

        public async Task<Result<ContentDetailedDTO>> GetContentById(string contentId)
        {
            try
            {
                var content = await _courseContentRepository.GetContentById(contentId);

                if (content == null)
                    return new Result<ContentDetailedDTO>
                    {
                        Success = false,
                        Errors = new string[] { $"Category with this id {contentId} not found." }
                    };

                return new Result<ContentDetailedDTO>
                {
                    Success = true,
                    Data = CreateCourseDetailedDTO(content)
                };
            }
            catch (Exception ex) { return new Result<ContentDetailedDTO>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                }; 
            }
        }

        public Task<Result<ContentDTO>> CreateContent(ContentCreationDTO contentDTO)
        {
            throw new NotImplementedException();
        }
        
        private static ContentDetailedDTO CreateCourseDetailedDTO(Content content)
        {
            return new ContentDetailedDTO 
            {
				Id = content.Id,
				Title = content.Title,
				Duration = content.Duration,
				CourseId = content.CourseId,
				Completed = content.Completed,
				IsDownloadable = content.IsDownloadable,
				Percentage = content.Percentage,
				PublicId = content.PublicId,
				Type = content.Type.ToString(),
				ContentUrl = content.Url
			};
        }

		private static ContentDTO CreateCourseDTO(Content content)
		{
			return new ContentDTO
			{
				Id = content.Id,
				Title = content.Title,
				Duration = content.Duration,
				IsDownloadable = content.IsDownloadable,
				ContentUrl = content.Url
			};

		}

        
    }
}
