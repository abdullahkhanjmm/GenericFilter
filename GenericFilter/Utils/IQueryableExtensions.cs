using System.Linq.Expressions;
using System.Reflection;
using GenericFilter.DTOs.Inputs;

namespace GenericFilter.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            if (parameters.FilterBy != null && !string.IsNullOrWhiteSpace(parameters.FilterValue))
            {
                query = ApplyFilters(query, parameters.FilterBy, parameters.FilterValue, parameters.FilterOperation);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = ApplySorting(query, parameters.SortBy, parameters.IsAscending);
            }

            return query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                        .Take(parameters.PageSize);
        }

        private static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, List<string> filterBy, string filterValue, FilterOperations filterOperation)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            foreach (var propertyName in filterBy)
            {
                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                {
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'.");
                }

                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var constant = Expression.Constant(filterValue);

                Expression comparison = filterOperation switch
                {
                    FilterOperations.Equals => Expression.Equal(propertyAccess, constant),
                    FilterOperations.NotEquals => Expression.NotEqual(propertyAccess, constant),
                    FilterOperations.Contains => Expression.Call(propertyAccess, "Contains", null, constant),
                    FilterOperations.StartsWith => Expression.Call(propertyAccess, "StartsWith", null, constant),
                    FilterOperations.EndsWith => Expression.Call(propertyAccess, "EndsWith", null, constant),
                    FilterOperations.GreaterThan => Expression.GreaterThan(propertyAccess, constant),
                    FilterOperations.LessThan => Expression.LessThan(propertyAccess, constant),
                    FilterOperations.GreaterThanOrEqual => Expression.GreaterThanOrEqual(propertyAccess, constant),
                    FilterOperations.LessThanOrEqual => Expression.LessThanOrEqual(propertyAccess, constant),
                    _ => throw new ArgumentException("Invalid filter operation")
                };

                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }

            return query;
        }


        private static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortBy, bool isAscending)
        {
            var type = typeof(T);
            var property = type.GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"No property '{sortBy}' found on type '{type.Name}'.");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var methodName = isAscending ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }

}
