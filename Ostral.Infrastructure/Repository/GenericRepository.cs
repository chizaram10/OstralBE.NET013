using Microsoft.EntityFrameworkCore;
using Ostral.Core.Interfaces;
using System.Data.Common;

namespace Ostral.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly OstralDBContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(OstralDBContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _db.FindAsync(id);
            if (entity == null) return;
            _db.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task InsertAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Update(T item)
        {
            _db.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}