using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
	public interface IStudentService
	{
		Task<Result<StudentDTO>> GetStudentById(string id);
	}
}
