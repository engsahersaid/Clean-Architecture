using System.Linq.Expressions;

namespace TicketManagement.Application.Specification
{
    public interface ISpecifications<T>
    {
        //This is where you can add in the expressions based on your entity
        Expression<Func<T, bool>> FilterCondition { get; }

        //If you want to include foreign keyed table data, you could add it using this method
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
    }
}
