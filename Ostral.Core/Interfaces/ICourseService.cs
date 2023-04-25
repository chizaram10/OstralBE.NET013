using Ostral.Core.DTOs;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
	public interface ICourseService
	{
		Task<Result<IEnumerable<CourseDTO>>> GetAllCourses(int pageSize, int pageNumber);

		Task<Result<IEnumerable<CourseDTO>>> GetTutorCoursesById(string tutorId, int pageSize, int pageNumber);

		Task<Result<IEnumerable<CourseDTO>>> GetCategoryCoursesById(string categoryId, int pageSize, int pageNumber);

		Task<Result<CourseDetailedDTO>> GetCourseById(string id);

		Task<Result<CourseDetailedDTO>> GetRandomCourse();

        Task<Result<IEnumerable<CourseDTO>>> GetPopularCourses();
    }
}
