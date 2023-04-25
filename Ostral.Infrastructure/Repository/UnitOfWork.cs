using Microsoft.EntityFrameworkCore.Storage;
using Ostral.Core.Interfaces;

namespace Ostral.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly OstralDBContext _context;
        private IDbContextTransaction? _dbContextTransaction;


        public UnitOfWork(OstralDBContext context, IIdentityService identityService)
        {
            _context = context;
           
        }

        public async Task CreateTransaction()
        {
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (_dbContextTransaction == null) return;
            await _dbContextTransaction.CommitAsync();
        }

        public async Task Rollback()
        {
            if (_dbContextTransaction == null) return;

            await _dbContextTransaction.RollbackAsync();
            await _dbContextTransaction.DisposeAsync();
        }


        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _context.Dispose();

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}