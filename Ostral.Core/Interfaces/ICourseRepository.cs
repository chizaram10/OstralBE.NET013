using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
    public interface ICourseRepository
    {
	    Task<PaginatorResponseDTO<IEnumerable<Course>>> GetAllCourses(int pageSize = 10, int pageNumber = 1);

		Task<PaginatorResponseDTO<IEnumerable<Course>>> GetTutorCoursesById(string tutorId, int pageSize = 10, int pageNumber = 1);

		Task<PaginatorResponseDTO<IEnumerable<Course>>> GetCategoryCoursesById(string categoryId, int pageSize, int pageNumber);

		Task<Course> GetCourseById(string courseID);

		Task<Course> UpdateCourse(Course course, string id);
        
        Task<IEnumerable<Course>> GetPopularCourses();
        
        Task<Course> GetRandomCourse();
    }
}
