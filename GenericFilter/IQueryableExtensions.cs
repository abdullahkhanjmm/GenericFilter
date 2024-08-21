using System.Linq.Expressions;
using System.Reflection;

namespace GenericFilter
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Process<T>(this IQueryable<T> queryable, PaginationFilter filter)
        {
            // Apply filtering
            if (!string.IsNullOrEmpty(filter.filterBy))
            {
                var filterExpression = CreateFilterExpression<T>(filter.filterBy);
                queryable = queryable.AsEnumerable().Where(filterExpression.Compile()).AsQueryable();
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.orderby))
            {
                queryable = queryable.OrderBy(filter.orderby);
            }

            // Apply pagination
            var skip = (filter.pageNumber - 1) * filter.pageSize;
            queryable = queryable.Skip(skip).Take(filter.pageSize);

            return queryable;
        }

        private static Expression<Func<T, bool>> CreateFilterExpression<T>(string filterBy)
        {
            var parts = filterBy.Split('=');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid filter format. Expected 'PropertyName=value'.");

            var propertyName = parts[0];
            var value = parts[1];

            var type = typeof(T);
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"No property '{propertyName}' found on type '{type.Name}'.");

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);
            Expression equals;

            if (property.PropertyType == typeof(string))
            {
                var method = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) });
                var constant = Expression.Constant(value, typeof(string));
                var comparisonType = Expression.Constant(StringComparison.OrdinalIgnoreCase);
                equals = Expression.Call(propertyAccess, method, constant, comparisonType);
            }
            else
            {
                // For other types, compare directly
                var constant = Expression.Constant(Convert.ChangeType(value, property.PropertyType));
                equals = Expression.Equal(propertyAccess, constant);
            }

            return Expression.Lambda<Func<T, bool>>(equals, parameter);
        }



        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty)
        {
            var type = typeof(T);

            // Determine the sorting direction
            var ascending = true;
            var parts = orderByProperty.Split(' ');
            var propertyName = parts[0];
            if (parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                ascending = false;
            }

            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"No property '{propertyName}' found on type '{type.Name}'.");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // Use OrderBy or OrderByDescending based on the direction
            var resultExpression = ascending
                ? Expression.Call(typeof(Queryable), "OrderBy", new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression))
                : Expression.Call(typeof(Queryable), "OrderByDescending", new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }

    }
}
