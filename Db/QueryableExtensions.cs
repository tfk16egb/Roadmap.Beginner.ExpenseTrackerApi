using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Roadmap.Beginner.ExpenseTrackerApi.Db;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereDateInRange<T>(
        this IQueryable<T> source,
        Expression<Func<T, DateTimeOffset>> selector,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate)
    {
        if (startDate != null)
            source = source.Where(e =>
                EF.Property<DateTimeOffset>(e, ((MemberExpression)selector.Body).Member.Name) >= startDate);

        if (endDate != null)
            source = source.Where(e =>
                EF.Property<DateTimeOffset>(e, ((MemberExpression)selector.Body).Member.Name) <= endDate);

        return source;
    }
}
