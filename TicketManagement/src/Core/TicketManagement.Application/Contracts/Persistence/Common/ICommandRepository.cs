using TicketManagement.Domain.Common;

namespace TicketManagement.Application.Contracts.Persistence.Common
{
    public interface ICommandRepository<T> where T : BaseAuditableEntity
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(Guid id);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        void PermenemtDelete(Guid id);
        void PermenemtDelete(T entity);
        void PermenemtDelete(IEnumerable<T> entities);

        void AddAsync(T entity, CancellationToken cancellationToken = default);
        void AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}