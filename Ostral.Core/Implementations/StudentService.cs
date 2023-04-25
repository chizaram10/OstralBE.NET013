using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
	public class StudentService : IStudentService
	{
		private readonly IStudentRepository _studentRepository;

		public StudentService(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public async Task<Result<StudentDTO>> GetStudentById(string id)
		{
			var result = await _studentRepository.GetStudentById(id);
			if (result != null) return new Result<StudentDTO> { Success = true, Data = CreateStudentDTO(result) };
			
			return new Result<StudentDTO> { Success = false, Errors = new List<string> { $"Unable to find student with id '{id}'." } };
		}

		private static StudentDTO CreateStudentDTO(Student student)
		{
			return new StudentDTO
			{
				Id = student.Id,
				ImageUrl = student.User.ImageUrl,
				Name = $"{student.User.FirstName} {student.User.LastName}",
			};
		}
	}
}
