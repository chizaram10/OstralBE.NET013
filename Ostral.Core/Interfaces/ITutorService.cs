using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;
	
public interface ITutorService
{
	Task<Result<IEnumerable<TutorDTO>>> GetTutors(int pageSize, int pageNumber);
	Task<Result<IEnumerable<TutorDTO>>> GetPopularTutors();
	Task<Result<TutorDTO>> GetTutorById(string id);

}