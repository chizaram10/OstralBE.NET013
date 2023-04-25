using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryDTO>> GetCategoryById(string id)
    {
        var result = await _categoryRepository.GetCategoryById(id);

        if (result == null)
            return new Result<CategoryDTO>
            {
                Errors = new[]  { $"Category with this id '{id}' not found." }
            };

        return new Result<CategoryDTO>
        {
            Success = true,
            Data = CreateCategoryDTO(result)
        };
    }

    public async Task<Result<IEnumerable<CategoryDTO>>> GetAllCategories(int pageSize, int pageNumber)
    {
        var categories = await _categoryRepository.GetAllCategories(pageSize, pageNumber);
        
        if (categories.PageItems!.Any()) return new Result<IEnumerable<CategoryDTO>>
        {
            Success = true,
            Data = categories.PageItems!.Select(c => CreateCategoryDTO(c))
        };
        
        return new Result<IEnumerable<CategoryDTO>>
        {
            Success = false,
            Errors = new string[] { "No categories availabe." }
        };
    }

    private static CategoryDTO CreateCategoryDTO(Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
            ImageUrl = category.ImageUrl,
            CourseList = category.CourseList.Select(c => new CourseDTO
            {
				Id = c.Id,
				Duration = c.Duration,
				ImageUrl = c.ImageUrl,
				Name = c.Name,
				Price = c.Price,
				TutorFullName = $"{c.Tutor.User.FirstName} {c.Tutor.User.LastName}",
				ContentCount = c.ContentList.Count
			}),
        };
    }
}