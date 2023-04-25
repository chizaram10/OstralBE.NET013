using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public interface ICategoryService
{
    public Task<Result<CategoryDTO>> GetCategoryById(string Id);
    public Task<Result<IEnumerable<CategoryDTO>>> GetAllCategories(int pageSize, int pageNumber);
}