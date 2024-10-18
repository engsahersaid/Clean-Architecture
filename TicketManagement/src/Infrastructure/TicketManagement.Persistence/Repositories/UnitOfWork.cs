using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Contracts.Persistence.Common;
using TicketManagement.Domain.Common;
using TicketManagement.Persistence.Data;
using TicketManagement.Persistence.Repositories.Common;

namespace TicketManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _databaseContext;
        private bool _disposed;

        public UnitOfWork(AppDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ILookupRepository<T> LookupRepository<T>() where T : BaseLookupEntity, new()
        {
            return new LookupRepository<T>(_databaseContext);
        }

        public IQueryRepository<T> QueryRepository<T>() where T : BaseAuditableEntity, new()
        {
            return new QueryRepository<T>(_databaseContext);
        }

        public ICommandRepository<T> CommandRepository<T>() where T : BaseAuditableEntity, new()
        {
            return new CommandRepository<T>(_databaseContext);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return _databaseContext.SaveChanges();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _databaseContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}