using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
   
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

		[HttpGet("")]
		public async Task<IActionResult> GetAllCourses([FromQuery] int pageSize, [FromQuery] int pageNumber)
		{
			var result = await _courseService.GetAllCourses(pageSize, pageNumber);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCourseById([FromRoute]string id)
		{
			var result = await _courseService.GetCourseById(id);

			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));

			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}

        [HttpGet("popular-courses")]
        public async Task<IActionResult> GetPopularCourses()
        {
            var courses = await _courseService.GetPopularCourses();
            return courses.Success ?
                Ok(ResponseDTO<object>.Success(courses.Data!))
                : NotFound(courses.Errors) ;
        }

        [HttpGet("random-courses")]
        public async Task<IActionResult> GetRandomCourse()
        {
            var courses = await _courseService.GetRandomCourse();
			return courses.Success ?
				Ok(ResponseDTO<object>.Success(courses.Data!))
				: NotFound(courses.Errors);
		}
    }
}
