using TicketManagement.Application.Contracts.Persistence.Common;
using TicketManagement.Domain.Common;

namespace TicketManagement.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ILookupRepository<T> LookupRepository<T>() where T : BaseLookupEntity, new();
        IQueryRepository<T> QueryRepository<T>() where T : BaseAuditableEntity, new();
        ICommandRepository<T> CommandRepository<T>() where T : BaseAuditableEntity, new();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}