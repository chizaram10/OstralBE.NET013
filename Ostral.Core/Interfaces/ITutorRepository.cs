using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces;


public interface ITutorRepository
{
    Task<PaginatorResponseDTO<IEnumerable<Tutor>>> GetTutors(int pageSize = 10, int pageNumber = 1);
    Task<IEnumerable<Tutor>> GetPopularTutors();
    Task<Tutor> GetTutorById(string id);
}