using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

	[HttpGet("")]
	public async Task<IActionResult> GetAllCategories([FromQuery] int pageSize, [FromQuery] int pageNumber)
	{
		var result = await _categoryService.GetAllCategories(pageSize, pageNumber);
		if (result.Success)
			return Ok(ResponseDTO<object>.Success(result.Data!));

		return NotFound(ResponseDTO<object>.Fail(result.Errors));
	}

	[HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] string id)
    {
        var result = await _categoryService.GetCategoryById(id);
		if (result.Success)
			return Ok(ResponseDTO<object>.Success(result.Data!));

		return NotFound(ResponseDTO<object>.Fail(result.Errors));
	}
}