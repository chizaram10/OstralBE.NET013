using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
	public class StudentCourseService : IStudentCourseService
	{
		private readonly IStudentCourseRepository _studentCourseRepository;
		private readonly ICourseRepository _courseRepository;
		private readonly IStudentRepository _studentRepository;

		public StudentCourseService(IStudentCourseRepository studentCourseRepository, ICourseRepository courseRepository,
			IStudentRepository studentRepository)
		{
			_studentCourseRepository = studentCourseRepository;
			_courseRepository = courseRepository;
			_studentRepository = studentRepository;
		}

		public async Task<Result<IEnumerable<StudentCourseDTO>>> GetAllStudentCourses(string studentId, int pageSize, int pageNumber)
		{
			var courses = await _studentCourseRepository.GetAllStudentCourses(studentId, pageSize, pageNumber);

			if (!courses.PageItems!.Any()) return new Result<IEnumerable<StudentCourseDTO>>
			{
				Success = true,
				Data = courses.PageItems!.Select(sc => CreateStudentCourseDTO(sc))
			};
			
			return new Result<IEnumerable<StudentCourseDTO>> 
			{ 
				Success = false, 
				Errors = new string[] { $"No courses found for student with id '{studentId}'"} 
			};
		}

		public async Task<Result<StudentCourseDTO>> EnrollForCourse(string courseId, string studentId)
		{
			Course course = await _courseRepository.GetCourseById(courseId);
			Student student = await _studentRepository.GetStudentById(studentId);

			if (student == null || course == null) return new Result<StudentCourseDTO>
			{
				Success = false,
				Errors = new[] { $"Unable to enroll for student with id '{studentId}' into course with id '{courseId}'." }
			};

			var isStudentEnrolled = _studentCourseRepository.GetStudentCourse(studentId, courseId) != null;

			if (isStudentEnrolled) return new Result<StudentCourseDTO>
			{
				Success = false,
				Errors = new[] { $"Already enrolled student with id '{studentId}' into course with id '{courseId}'." }
			};

			StudentCourse studentCourse = new StudentCourse
			{
				Course = course,
				Student = student,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			await _studentCourseRepository.Add(studentCourse);

			return new Result<StudentCourseDTO> { Success = true, Data = CreateStudentCourseDTO(studentCourse) };
		}

		private static StudentCourseDTO CreateStudentCourseDTO(StudentCourse studentCourse)
		{
			return new StudentCourseDTO
			{
				Id = studentCourse.Id,
				StudentId = studentCourse.StudentId,
				CourseId = studentCourse.CourseId,
				CategoryName = studentCourse.Course.Category.Name,
				CourseName = studentCourse.Course.Name,
				CompletionDate = studentCourse.CompletionDate,
				CompletionPercentage = studentCourse.CompletionPercentage
			};
		}
	}
}
