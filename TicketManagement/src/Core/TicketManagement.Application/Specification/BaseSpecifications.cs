using System.Linq.Expressions;

namespace TicketManagement.Application.Specification
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        private readonly List<Expression<Func<T, object>>> _includeCollection = new List<Expression<Func<T, object>>>();

        public BaseSpecifications()
        {
            this.FilterCondition = e => true;
        }

        public BaseSpecifications(Expression<Func<T, bool>> filterCondition)
        {
            this.FilterCondition = filterCondition;
        }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = this.FilterCondition.Compile();
            return predicate(entity);
        }

        public BaseSpecifications<T> And(BaseSpecifications<T> specification)
        {
            var spec = new AndSpecification<T>(this, specification);
            foreach (var include in this.Includes)
            {
                spec.AddInclude(include);
            }

            foreach (var include in specification.Includes)
            {
                spec.AddInclude(include);
            }
            return spec;
        }

        public BaseSpecifications<T> And(Expression<Func<T, bool>> filterCondition)
        {
            var spec = this.And(new BaseSpecifications<T>(filterCondition));
            return spec;
        }

        public BaseSpecifications<T> Or(BaseSpecifications<T> specification)
        {
            var spec = new OrSpecification<T>(this, specification);
            foreach (var include in this.Includes)
            {
                spec.AddInclude(include);
            }

            foreach (var include in specification.Includes)
            {
                spec.AddInclude(include);
            }
            return spec;
        }

        public BaseSpecifications<T> Or(Expression<Func<T, bool>> filterCondition)
        {
            var spec = this.Or(new BaseSpecifications<T>(filterCondition));
            return spec;
        }

        public virtual Expression<Func<T, bool>> FilterCondition { get; private set; }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public List<Expression<Func<T, object>>> Includes
        {
            get
            {
                return _includeCollection;
            }
        }

        public Expression<Func<T, object>> GroupBy { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void SetFilterCondition(Expression<Func<T, bool>> filterExpression)
        {
            FilterCondition = filterExpression;
        }

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}