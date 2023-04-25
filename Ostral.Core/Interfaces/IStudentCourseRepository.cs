using Ostral.Core.DTOs;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
	public interface IStudentCourseRepository
	{
		Task<PaginatorResponseDTO<IEnumerable<StudentCourse>>> GetAllStudentCourses(string studentId, int pageSize, int pageNumber);
		Task<StudentCourse> GetStudentCourse(string studentId, string courseId);
		Task<StudentCourse> Add(StudentCourse studentCourse);
	}
}