using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketManagement.Application.Contracts.Persistence.Common;
using TicketManagement.Application.Models;
using TicketManagement.Application.Specification;
using TicketManagement.Domain.Common;
using TicketManagement.Infrastructure.Specification;
using TicketManagement.Persistence.Data;

namespace TicketManagement.Persistence.Repositories.Common
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseAuditableEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly IQueryable<T> _dbSetQuery;

        public QueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetQuery = _dbContext.Set<T>().AsNoTracking();
        }

        public T? GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T? Get(Expression<Func<T, bool>> expression, string includeProperties = "")
        {
            var query = _dbSetQuery;

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));

            return query.SingleOrDefault(expression);
        }

        public List<T> GetAll(string includeProperties = "")
        {
            var query = _dbSetQuery;
            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));
            return query.ToList();
        }

        public List<T> GetList(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            var query = _dbSetQuery;
            query = expression != null ? query.Where(expression) : query;

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public PageResult<T> GetList(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        {
            var query = _dbSetQuery;
            query = expression != null ? query.Where(expression) : query;
            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));

            query = orderBy != null ? orderBy(query) : query;

            long totalCount = query.Count();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageResult<T>(pageSize, pageIndex, totalCount, items);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public Task<T?> GetAsync(Expression<Func<T, bool>> expression, string includeProperties = "", CancellationToken cancellationToken = default)
        {
            var query = _dbSetQuery;

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                 => current.Include(includeProperty));

            return query.SingleOrDefaultAsync(expression, cancellationToken);
        }

        public Task<List<T>> GetAllAsync(string includeProperties = "", CancellationToken cancellationToken = default)
        {
            var query = _dbSetQuery;
            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));
            return query.ToListAsync(cancellationToken);
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default)
        {
            var query = _dbSetQuery;
            query = expression != null ? query.Where(expression) : query;
            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToListAsync(cancellationToken) : query.ToListAsync(cancellationToken);
        }

        public async Task<PageResult<T>> GetListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var query = _dbSetQuery;
                query = expression != null ? query.Where(expression) : query;
                query = includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                        => current.Include(includeProperty));

                query = orderBy != null ? orderBy(query) : query;

                long totalCount = query.Count();
                var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PageResult<T>(pageSize, pageIndex, totalCount, items);
            });
        }

        public PageResult<T> FindWithSpecificationPattern(ISpecifications<T> specification = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbSetQuery.AsQueryable().AsNoTracking(), specification);
            long totalCount = query.Count();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageResult<T>(pageSize, pageIndex, totalCount, items);
        }
    }
}