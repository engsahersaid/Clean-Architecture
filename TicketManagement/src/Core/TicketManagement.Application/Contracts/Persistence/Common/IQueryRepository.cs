using System.Linq.Expressions;
using TicketManagement.Application.Models;
using TicketManagement.Application.Specification;
using TicketManagement.Domain.Common;

namespace TicketManagement.Application.Contracts.Persistence.Common
{
    public interface IQueryRepository<T> where T : BaseAuditableEntity
    {
        T? GetById(Guid id);

        T? Get(Expression<Func<T, bool>> expression, string includeProperties = "");

        List<T> GetAll(string includeProperties = "");

        List<T> GetList(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        PageResult<T> GetList(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10);

        //IQueryable<T> FindQueryable(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<T?> GetAsync(Expression<Func<T, bool>> expression, string includeProperties = "", CancellationToken cancellationToken = default);

        Task<List<T>> GetAllAsync(string includeProperties = "", CancellationToken cancellationToken = default);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default);

        Task<PageResult<T>> GetListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        PageResult<T> FindWithSpecificationPattern(ISpecifications<T> specification = null, int pageIndex = 1, int pageSize = 10);
    }
}