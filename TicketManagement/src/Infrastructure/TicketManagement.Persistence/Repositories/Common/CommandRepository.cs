using Microsoft.EntityFrameworkCore;
using TicketManagement.Application.Contracts.Persistence.Common;
using TicketManagement.Domain.Common;
using TicketManagement.Persistence.Data;

namespace TicketManagement.Persistence.Repositories.Common
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseAuditableEntity
    {
        private readonly AppDbContext _dbContext;

        public CommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.LastModifiedDate = DateTime.Now;
            _dbContext.Set<T>().Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                entity.LastModifiedDate = DateTime.Now;
            }
            _dbContext.Set<T>().UpdateRange(entities);
        }

        public void Delete(Guid id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity == null)
                throw new Exception("Entity Not found");
            Delete(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        public void PermenemtDelete(Guid id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity == null)
                throw new Exception("Entity Not found");
            PermenemtDelete(entity);
        }

        public void PermenemtDelete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void PermenemtDelete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }
    }
}