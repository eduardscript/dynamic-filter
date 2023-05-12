using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace DynamicFilter.Lib.EFCore;

public static class DynamicFilterEFCore
{
    public static async Task<IList<TEntity>> GetAll<TEntity>(this DbSet<TEntity> entities, DynamicFilter filter)
        where TEntity : class
    {
        IQueryable<TEntity> query = entities;

        if (filter.IsNull)
        {
            return await query.ToListAsync();
        }
        
        var predicate = PredicateBuilder.New<TEntity>();
        foreach (var property in filter.ValueProperties)
        {
            var value = property.GetValue(filter);

            var predicateParameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyExpression = Expression.Property(predicateParameter, property.Name);

            var isNullableType = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
            
            var valueExpression = Expression.Constant(value, (isNullableType ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType)!);
            var comparison = Expression.Equal(propertyExpression, valueExpression);

            predicate = predicate.And(Expression.Lambda<Func<TEntity, bool>>(comparison, predicateParameter));
        }

        return await query.Where(predicate)
            .ToListAsync();
    }
}