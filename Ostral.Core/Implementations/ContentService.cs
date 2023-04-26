using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using NReco.VideoConverter;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;
using System.Runtime.CompilerServices;

namespace Ostral.Core.Implementations
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly Cloudinary _cloudinary;
        private readonly FFMpegConverter _converter;


        public ContentService(IContentRepository courseContentRepository, ICourseRepository courseRepository,
            Cloudinary cloudinary, FFMpegConverter converter)
        {
            _contentRepository = courseContentRepository;
            _courseRepository = courseRepository;
            _cloudinary = cloudinary;
            _converter = converter;
        }

        public async Task<Result<IEnumerable<ContentDTO>>> GetAllCourseContentById(string courseId)
        {
            try
            {
                var courseContent = await _contentRepository.GetAllCourseContentById(courseId);

                if (courseContent.Any())
                    return new Result<IEnumerable<ContentDTO>>
                    {
                        Success = false,
                        Errors = new string[] { $"No content found for course '{courseId}'." }
                    };

                return new Result<IEnumerable<ContentDTO>>
                {
                    Success = true,
                    Data = courseContent.Select(c => CreateContentDTO(c))
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
                var content = await _contentRepository.GetContentById(contentId);

                if (content == null)
                    return new Result<ContentDetailedDTO>
                    {
                        Success = false,
                        Errors = new string[] { $"Category with this id {contentId} not found." }
                    };

                return new Result<ContentDetailedDTO>
                {
                    Success = true,
                    Data = CreateContentDetailedDTO(content)
                };
            }
            catch (Exception ex) { return new Result<ContentDetailedDTO>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                }; 
            }
        }

        public async Task<Result<ContentDTO>> CreateContent(ContentCreationDTO data, string courseId)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(data.File!.FileName, data.File.OpenReadStream())
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                var contentUrl = uploadResult.SecureUrl.AbsoluteUri;

                var content = new Content()
                {
                    Title = data.Title,
                    ContentType = data.File.ContentType,
                    Url = contentUrl,
                    Duration = data.Duration,
                    Course = await _courseRepository.GetCourseById(courseId),
                    IsDownloadable = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _contentRepository.AddContent(content);

                return new Result<ContentDTO>
                {
                    Data = CreateContentDTO(content),
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new Result<ContentDTO> { Errors = new[] { ex.Message } };
            }
        }
        
        private static ContentDetailedDTO CreateContentDetailedDTO(Content content)
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
				ContentType = content.ContentType,
				ContentUrl = content.Url
			};
        }

		private static ContentDTO CreateContentDTO(Content content)
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
