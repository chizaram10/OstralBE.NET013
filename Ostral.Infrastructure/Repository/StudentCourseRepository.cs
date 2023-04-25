using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
	public class StudentCourseRepository : IStudentCourseRepository
	{
		private readonly OstralDBContext _context;
		private readonly IMapper _mapper;

		public StudentCourseRepository(OstralDBContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatorResponseDTO<IEnumerable<StudentCourse>>> GetAllStudentCourses(string studentId, int pageSize, int pageNumber)
		{
			var courses = _context.StudentCourses
				.Where(sc => sc.StudentId == studentId)
				.Include(sc => sc.Course)
				.Include(sc => sc.Course.Category);

			return await courses.PaginationAsync<StudentCourse, StudentCourse>(pageSize, pageNumber, _mapper);
		}

		public async Task<StudentCourse> GetStudentCourse(string studentId, string courseId)
		{
			var course = await _context.StudentCourses
				.FirstOrDefaultAsync();

			return course!;
		}

		public async Task<StudentCourse> Add(StudentCourse studentCourse)
		{
			await _context.StudentCourses.AddAsync(studentCourse);
			await _context.SaveChangesAsync();
			return studentCourse;
		}
	}
}