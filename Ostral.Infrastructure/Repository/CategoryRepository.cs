using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly OstralDBContext _context;
	private readonly IMapper _mapper;

    public CategoryRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
		_mapper = mapper;
    }

    public async Task<PaginatorResponseDTO<IEnumerable<Category>>> GetAllCategories(int pageSize, int pageNumber)
    {
		var categories = _context.Categories
				.Include(c => c.CourseList);
		return await categories.PaginationAsync<Category, Category>(pageSize, pageNumber, _mapper);
	}

	public async Task<Category> GetCategoryById(string Id)
	{
		var result =  await _context.Categories
            .Where(c => Id == c.Id)
			.Include(c => c.CourseList)
			.FirstOrDefaultAsync(c => Id == c.Id);
		return result!;
	}
}