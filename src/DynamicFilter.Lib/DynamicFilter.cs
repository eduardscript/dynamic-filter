using System.Reflection;

namespace DynamicFilter.Lib;

public abstract class DynamicFilter
{
    private IEnumerable<PropertyInfo> FilteredProperties => GetType()
        .GetProperties()
        .Where(
            p => p.Name != nameof(IsNull) &&
                 p.Name != nameof(FilteredProperties) &&
                 p.Name != nameof(ValueProperties))
        .ToList();

    public IEnumerable<PropertyInfo> ValueProperties => FilteredProperties
        .Where(x => x.GetValue(this) is not null)
        .ToList();
    
    public bool IsNull => !ValueProperties.Any();
}