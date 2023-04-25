using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces;

public interface ICategoryRepository
{
	Task<PaginatorResponseDTO<IEnumerable<Category>>> GetAllCategories(int pageSize = 10, int pageNumber = 1);
	Task<Category?> GetCategoryById(string Id);
}