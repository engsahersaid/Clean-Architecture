using Microsoft.EntityFrameworkCore;
using TicketManagement.Application.Contracts.Persistence.Common;
using TicketManagement.Domain.Common;
using TicketManagement.Persistence.Data;

namespace TicketManagement.Persistence.Repositories.Common
{
    public class LookupRepository<T> : ILookupRepository<T> where T : BaseLookupEntity
    {
        private readonly AppDbContext _dbContext;

        public LookupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Set<T>().ToListAsync(cancellationToken);
        }
    }
}