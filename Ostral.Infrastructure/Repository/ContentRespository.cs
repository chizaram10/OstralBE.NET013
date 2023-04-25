
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
    public class ContentRespository : IContentRepository
    {
        private readonly OstralDBContext _context;

        public ContentRespository(OstralDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Content>> GetAllCourseContentById(string courseId)
        {
            var contents = await _context.Contents
                .Where(c => courseId == c.CourseId)
                .ToListAsync();

            return contents;
        }

        public async Task<Content> GetContentById(string contentId)
        {
            var content = await _context.Contents
                .FirstOrDefaultAsync(c => contentId == c.Id);

            return content!;
        }

        public async Task AddContent(Content content)
        {
            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();
        }
    }
}
