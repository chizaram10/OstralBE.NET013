using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository;

public class TutorRepository : ITutorRepository
{
    private readonly OstralDBContext _context;
    private readonly IMapper _mapper;

    public TutorRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatorResponseDTO<IEnumerable<Tutor>>> GetTutors(int pageSize, int pageNumber)
    {
        var tutors = _context.Tutors
            .Include(t => t.CourseList)
            .Include(t => t.User);

        return await tutors.PaginationAsync<Tutor, Tutor>(pageSize, pageNumber, _mapper);
    }
    
    public async Task<IEnumerable<Tutor>> GetPopularTutors()
    {
        var tutors = await _context.Tutors
            .Include(t => t.CourseList)
            .Include(t => t.User)
            .OrderBy(t => t.CourseList.Sum(c => c.StudentList.Count))
            .Take(6)
            .ToListAsync();

        return tutors;
    }

	public async Task<Tutor> GetTutorById(string id)
	{
		var tutor = await _context.Tutors
            .Where(t => id == t.Id)
			.Include(t => t.CourseList)
			.Include(t => t.User)
			.FirstOrDefaultAsync(t => id == t.Id);

		return tutor!;
	}
}