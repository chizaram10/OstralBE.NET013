using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

		public CourseService(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}

		public async Task<Result<IEnumerable<CourseDTO>>> GetAllCourses(int pageSize, int pageNumber)
		{
			var result = await _courseRepository.GetAllCourses(pageSize, pageNumber);

			if (result == null || result.PageItems!.Count() == 0) return new Result<IEnumerable<CourseDTO>>
			{
				Success = false,
				Errors = new string[] { "No courses found." }
			};

			var courses = result.PageItems!.Select(x => CreateCourseDTO(x));

			return new Result<IEnumerable<CourseDTO>>
			{
				Data = courses,
				Success = true
			};
		}

		public async Task<Result<IEnumerable<CourseDTO>>> GetTutorCoursesById(string tutorId, int pageSize, int pageNumber)
		{
			var result = await _courseRepository.GetTutorCoursesById(tutorId, pageSize, pageNumber);

			if (result == null || result.PageItems!.Count() == 0) return new Result<IEnumerable<CourseDTO>>
			{
				Success = false,
				Errors = new string[] { $"No courses found for tutor with id '{tutorId}'" }
			};

			var courses = result.PageItems!.Select(x => CreateCourseDTO(x));

			return new Result<IEnumerable<CourseDTO>>
			{
				Data = courses,
				Success = true
			};
		}

		public async Task<Result<IEnumerable<CourseDTO>>> GetCategoryCoursesById(string categoryId, int pageSize, int pageNumber)
		{
			var result = await _courseRepository.GetCategoryCoursesById(categoryId, pageSize, pageNumber);

			if (result == null || result.PageItems!.Count() == 0) return new Result<IEnumerable<CourseDTO>>
			{
				Success = false,
				Errors = new string[] { $"No courses found for category with id '{categoryId}'" }
			};

			var courses = result.PageItems!.Select(x => CreateCourseDTO(x));

			return new Result<IEnumerable<CourseDTO>>
			{
				Data = courses,
				Success = true
			};
		}

		public async Task<Result<CourseDetailedDTO>> GetCourseById(string id)
		{
			try
			{
                var course = await _courseRepository.GetCourseById(id);
                if (course == null) return new Result<CourseDetailedDTO>
                {
                    Success = false,
                    Errors = new string[] { $"Course with this id '{id}' not found." }
                };

                return new Result<CourseDetailedDTO>
                {
                    Data = CreateCourseDetailedDTO(course),
                    Success = true
                };
            }
            catch(Exception ex) { return new Result<CourseDetailedDTO>
                {
                    Success = false,
                    Errors = (new string[] { ex.Message })
                };
            }
		}

        public async Task<Result<IEnumerable<CourseDTO>>> GetPopularCourses()
        {
            var  popularCourses = await _courseRepository.GetPopularCourses();
            if (popularCourses == null || popularCourses.Count() == 0) return new Result<IEnumerable<CourseDTO>>
            {
                Success = false,
                Errors = new List<string> { "No courses found" }
            };

            return new Result<IEnumerable<CourseDTO>> { Success = true, Data = popularCourses.Select(c => CreateCourseDTO(c)) };
        }

        public async Task<Result<CourseDetailedDTO>> GetRandomCourse()
        {
            var randomCourses = await _courseRepository.GetRandomCourse();
            if (randomCourses == null) return new Result<CourseDetailedDTO>
            {
                Success = false,
                Errors = new List<string> { $"No courses found." }
            };

            return new Result<CourseDetailedDTO> { Success = true, Data = CreateCourseDetailedDTO(randomCourses) };
        }

        private static CourseDTO CreateCourseDTO (Course course)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Duration = course.Duration,
                ImageUrl = course.ImageUrl,
                Name = course.Name,
                Price = course.Price,
                TutorFullName = $"{course.Tutor.User.FirstName} {course.Tutor.User.LastName}",
                ContentCount = course.ContentList.Count
            };
        }

        private static CourseDetailedDTO CreateCourseDetailedDTO(Course course)
        {
			return new CourseDetailedDTO
			{
				Id = course.Id,
				Name = course.Name,
				Description = course.Description,
				TutorFullName = $"{course.Tutor.User.FirstName} {course.Tutor.User.LastName}",
				TutorDescription = course.Tutor.Profession,
				TutorId = course.TutorId,
				TutorImageUrl = course.Tutor.User.ImageUrl,
				ImageUrl = course.ImageUrl,
				Price = course.Price,
				Duration = course.Duration,
				Completed = course.Completed,
				CategoryId = course.CategoryId,
				CategoryName = course.Category.Name,
				StudentCount = course.StudentList.Count(),
				ContentCount = course.ContentList.Count
			};
		}
	}
}
