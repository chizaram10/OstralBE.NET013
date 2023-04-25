using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
	[ApiController]
	[Route("api/course/{courseId}/content")]
	public class CourseContentController : ControllerBase
	{
		private readonly IContentService _courseContentService;

		public CourseContentController(IContentService courseContentService)
		{
			_courseContentService = courseContentService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllCourseContent([FromRoute] string courseId)
		{
			var result = await _courseContentService.GetAllCourseContentById(courseId);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}
	}
}
