using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
	[ApiController]
	[Route("api/student/{studentId}/course")]
	public class StudentCourseController : ControllerBase
	{
		private readonly IStudentCourseService _studentCourseService;

		public StudentCourseController(IStudentCourseService studentCourseService)
		{
			_studentCourseService = studentCourseService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllStudentCourses([FromRoute]string studentId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
		{
			var result = await _studentCourseService.GetAllStudentCourses(studentId, pageSize, pageNumber);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}

		[HttpPost("{courseId}")]
		public async Task<IActionResult> EnrollForCourse([FromRoute] string courseId, [FromRoute] string studentId)
		{
			var result = await _studentCourseService.EnrollForCourse(courseId, studentId);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(result.Errors));
		}

	}
}
