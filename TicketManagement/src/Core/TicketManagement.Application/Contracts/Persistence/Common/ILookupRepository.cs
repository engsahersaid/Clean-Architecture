using TicketManagement.Domain.Common;

namespace TicketManagement.Application.Contracts.Persistence.Common
{
    public interface ILookupRepository<T> where T : BaseLookupEntity
    {
        List<T> GetAll();

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}