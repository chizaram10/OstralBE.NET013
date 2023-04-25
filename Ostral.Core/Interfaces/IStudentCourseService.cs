using Ostral.Core.DTOs;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
    public interface IStudentCourseService
    {
        Task<Result<IEnumerable<StudentCourseDTO>>> GetAllStudentCourses(string studentId, int pageSize, int pageNumber);
        Task<Result<StudentCourseDTO>> EnrollForCourse(string courseId, string studentId);

	}
}