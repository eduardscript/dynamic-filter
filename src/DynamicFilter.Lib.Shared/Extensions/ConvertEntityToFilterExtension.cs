namespace DynamicFilter.Lib.Shared.Extensions;

public static class ConvertEntityToFilterExtension
{
    public static TFilter ConvertEntityValuesToFilterValuesExtension<TEntity, TFilter>(this TEntity entity, TFilter filter)
        where TEntity : class 
        where TFilter : DynamicFilter
    {
        var entityProperties = entity.GetType()
            .GetProperties();

        foreach (var entityProperty in entityProperties)
        {
            var entityPropertyName = entityProperty.Name;
            var entityPropertyValue = entityProperty.GetValue(entity);
            
            filter.GetType().GetProperty(entityPropertyName)!.SetValue(filter, entityPropertyValue);
        }

        return filter;
    }
}