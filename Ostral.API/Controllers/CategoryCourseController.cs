using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("/api/category/{categoryId}/course")]
public class CategoryCourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CategoryCourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCategoryCourses([FromRoute] string categoryId, [FromQuery] int pageSize,
        [FromQuery] int pageNumber)
    {
        var result = await _courseService.GetCategoryCoursesById(categoryId, pageSize, pageNumber);
        if (result.Success)
            return Ok(ResponseDTO<object>.Success(result.Data!));

        return NotFound(ResponseDTO<object>.Fail(result.Errors));
    }
}