using System.Linq.Dynamic.Core;

namespace SpecificationPatternHelperApp.Concrete;

public static class SpecificationBuilderExtensions
{
    public static IQueryable<TEntity> DynamicWhereExtension<TEntity>(
        this IQueryable<TEntity> query,
        string filter = null,
        string orderBy = null,
        string orderByDescending = null,
        int? pageIndex = null,
        int? pageSize = null) where TEntity : class
    {
        if (!string.IsNullOrWhiteSpace(filter)) query = query.Where(filter);

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            query = query.OrderBy(orderBy);
        }
        else if (!string.IsNullOrWhiteSpace(orderByDescending))
        {
            // TKey türünü almak için sıralama ifadesini oluştur
            var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
            var orderByDescendingExpression =
                Expression.Lambda(Expression.Property(parameterExpression, orderByDescending), parameterExpression);
            var resultExpression = Expression.Call(typeof(Queryable), "OrderByDescending",
                new[] { typeof(TEntity), orderByDescendingExpression.ReturnType }, query.Expression,
                orderByDescendingExpression);

            // Sorguyu güncelle
            query = query.Provider.CreateQuery<TEntity>(resultExpression);
        }

        if (pageIndex.HasValue && pageSize.HasValue)
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);

        return query;
    }
}

public static class SpecificationBuilderExtensionsV2
{
    public static IQueryable<TEntity> DynamicWhereExtensionV2<TEntity>(
        this IQueryable<TEntity> query,
        DynamicFilterHelper dynamicFilterHelper) where TEntity : class
    {
        if (!string.IsNullOrWhiteSpace(dynamicFilterHelper.Filter)) query = query.Where(dynamicFilterHelper.Filter);

        if (!string.IsNullOrWhiteSpace(dynamicFilterHelper.OrderBy))
        {
            query = query.OrderBy(dynamicFilterHelper.OrderBy);
        }
        else if (!string.IsNullOrWhiteSpace(dynamicFilterHelper.OrderByDescending))
        {
            // TKey türünü almak için sıralama ifadesini oluştur
            var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
            var orderByDescendingExpression =
                Expression.Lambda(Expression.Property(parameterExpression, dynamicFilterHelper.OrderByDescending),
                    parameterExpression);
            var resultExpression = Expression.Call(typeof(Queryable), "OrderByDescending",
                new[] { typeof(TEntity), orderByDescendingExpression.ReturnType }, query.Expression,
                orderByDescendingExpression);

            // Sorguyu güncelle
            query = query.Provider.CreateQuery<TEntity>(resultExpression);
        }

        if (dynamicFilterHelper.PageIndex.HasValue && dynamicFilterHelper.PageSize.HasValue)
            query = query.Skip((dynamicFilterHelper.PageIndex.Value - 1) * dynamicFilterHelper.PageSize.Value)
                .Take(dynamicFilterHelper.PageSize.Value);

        return query;
    }
}

