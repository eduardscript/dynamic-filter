namespace DynamicFilter.Lib.Extensions;

public static class ConvertBetweenEntityAndFilterExtension
{
    public static TFilter ToFilter<TEntity, TFilter>(this TEntity entity, TFilter filter)
        where TEntity : class
        where TFilter : DynamicFilter
    {
        return CloneTo(entity, filter);
    }
    
    public static TEntity ToEntity<TEntity, TFilter>(this TFilter filter, TEntity entity)
        where TFilter : DynamicFilter
        where TEntity : class
    {
        return CloneTo(filter, entity);
    }
    
    private static TTo CloneTo<TFrom, TTo>(this TFrom from, TTo to)
    {
        var entityProperties = from!.GetType()
            .GetProperties();

        var toType = to!.GetType();
        foreach (var entityProperty in entityProperties)
        {
            var entityPropertyName = entityProperty.Name;
            var entityPropertyValue = entityProperty.GetValue(from);

            var propertyExists = toType.GetProperty(entityPropertyName);
            
            propertyExists?.SetValue(to, entityPropertyValue);
        }

        return to;
    }
}