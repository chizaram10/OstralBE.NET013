namespace Ostral.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task CreateTransaction();
        void Dispose();
        Task Rollback();
        Task Save();
    }
}