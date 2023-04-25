using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/tutor/{tutorId}/course")]
public class TutorCourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public TutorCourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetTutorCourses([FromRoute] string tutorId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var result = await _courseService.GetTutorCoursesById(tutorId, pageSize, pageNumber);
        return result.Success ? 
            Ok(ResponseDTO<object>.Success(result.Data!)) 
            : NotFound(ResponseDTO<object>.Fail(result.Errors));
    }

    [HttpPost("create-course/category/{categoryId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateCourse([FromRoute] string tutorId, [FromRoute] string categoryId, [FromForm] CourseCreationDTO data)
    {
        var result = await _courseService.CreateCourse(data, tutorId, categoryId);
        return result.Success ?
            Ok(ResponseDTO<object>.Success(result.Data!))
            : BadRequest(ResponseDTO<object>.Fail(result.Errors));
    }
}