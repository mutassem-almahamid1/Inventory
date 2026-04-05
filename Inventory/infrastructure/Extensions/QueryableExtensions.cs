using Microsoft.EntityFrameworkCore;
using Shared.Requests;
using Shared.Responses;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> source,
        PagedRequest request,
        CancellationToken cancellationToken = default)
    {
        return await source.ToPagedResponseAsync(
            request.PageNumber,
            request.PageSize,
            request.OrderBy,
            request.SortDescending,
            cancellationToken);
    }

    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        string? orderBy = null,
        bool sortDescending = false,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var property = typeof(T).GetProperty(orderBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                var methodName = sortDescending ? "OrderByDescending" : "OrderBy";
                var resultExpression = Expression.Call(typeof(Queryable), methodName,
                    new Type[] { typeof(T), property.PropertyType },
                    source.Expression, Expression.Quote(orderByExpression));

                source = source.Provider.CreateQuery<T>(resultExpression);
            }
        }

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>(items, count, pageNumber, pageSize);
    }
}