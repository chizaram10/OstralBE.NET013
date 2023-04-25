using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _courseContentService;

        public ContentController(IContentService courseContentService)
        {
            _courseContentService = courseContentService;
        }
        
        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetCourseContentById([FromRoute] string contentId)
        {
            var result = await _courseContentService.GetContentById(contentId);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}
    }
}
