using Microsoft.EntityFrameworkCore;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
	public class StudentRepository : IStudentRepository
	{
		private readonly OstralDBContext _context;

		public StudentRepository(OstralDBContext context)
		{
			_context = context;
		}

		public async Task<Student> GetStudentById(string id)
		{
			var result = await _context.Students
				.Include(s => s.CourseList)
				.Include(s => s.User)
				.FirstOrDefaultAsync(s => id == s.Id);
			return result!;
		}

		public async Task<Student> AddStudent(Student student)
		{
			await _context.Students.AddAsync(student);
			await _context.SaveChangesAsync();
			return student;
		}
	}
}
