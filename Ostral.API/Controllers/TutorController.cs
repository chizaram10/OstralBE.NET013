using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/tutor")]
public class TutorController: ControllerBase
{
    private readonly ITutorService _tutorService;

    public TutorController(ITutorService tutorService)
    {
        _tutorService = tutorService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetTutors([FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var result = await _tutorService.GetTutors(pageSize, pageNumber);
        
        if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
        return NotFound(ResponseDTO<object>.Fail(result.Errors));
    }


	[HttpGet("{id}")]
	public async Task<IActionResult> GetTutorById([FromRoute] string id)
    {
		var result = await _tutorService.GetTutorById(id);

		if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
        return NotFound(ResponseDTO<object>.Fail(result.Errors));
	}

	[HttpGet("popular-tutors")]
    public async Task<IActionResult> GetPopularTutors()
    {
        var result = await _tutorService.GetPopularTutors();

		if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
		return NotFound(ResponseDTO<object>.Fail(result.Errors));
	}
}