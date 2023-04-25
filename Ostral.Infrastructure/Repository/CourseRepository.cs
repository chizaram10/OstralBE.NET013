using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using AutoMapper;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OstralDBContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(OstralDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatorResponseDTO<IEnumerable<Course>>> GetAllCourses(int pageSize, int pageNumber)
        {
            var courses = _context.Courses
                .Include(c => c.ContentList)
                .Include(c => c.StudentList);
            return await courses.PaginationAsync<Course, Course>(pageSize, pageNumber, _mapper);
        }

		public async Task<PaginatorResponseDTO<IEnumerable<Course>>> GetCategoryCoursesById(string categoryId, int pageSize,
		int pageNumber)
		{
			var courses = _context.Courses
                .Where(course => categoryId == course.CategoryId)
				.Include(c => c.ContentList)
				.Include(c => c.StudentList);

			return await courses.PaginationAsync<Course, Course>(pageSize, pageNumber, _mapper);
		}

		public async Task<PaginatorResponseDTO<IEnumerable<Course>>> GetTutorCoursesById(string tutorId, int pageSize, int pageNumber)
		{
			var courses = _context.Courses
				.Where(course => tutorId == course.TutorId)
				.Include(c => c.ContentList)
				.Include(c => c.StudentList);

			return await courses.PaginationAsync<Course, Course>(pageSize, pageNumber, _mapper);
		}

		public async Task<Course> GetCourseById(string courseID)
        {
            var result = await _context.Courses
                .Include(c => c.ContentList)
                .Include(c => c.StudentList)
                .Include(c => c.Tutor)
                .Include(c => c.Tutor.User)
                .FirstOrDefaultAsync(c => courseID == c.Id);
            return result!;
        }

        public async Task<Course> UpdateCourse(Course course, string id)
        {
            var courseToUpdate = await GetCourseById(id);
            if (courseToUpdate == null) return courseToUpdate!;

			_context.Entry(course).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return courseToUpdate;
		}

        public async Task<IEnumerable<Course>> GetPopularCourses()
        {
            var popularCourses = await _context.Courses
				.Include(c => c.ContentList)
				.Include(c => c.StudentList)
				.OrderByDescending(c => c.StudentList.Count)
                .Take(6)
                .ToListAsync();

            return popularCourses;
        }

        public async Task<Course> GetRandomCourse()
        {
            var randomCourses = await _context.Courses
				.Include(c => c.ContentList)
				.Include(c => c.StudentList)
				.OrderBy(c => EF.Functions.Random())
                .FirstOrDefaultAsync();

            return randomCourses!;
        }

        public async Task AddCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }
    }
}