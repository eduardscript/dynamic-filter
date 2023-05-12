using System.Text;

namespace DynamicFilter.Lib.Extensions;

public static class BuildQueryStringExtension
{
    public static string? BuildQueryString(this DynamicFilter filter)
    {
        var valuePropertiesCount = filter.ValueProperties.Count();
        if (valuePropertiesCount == default)
        {
            return null;
        }

        var stringBuilder = new StringBuilder("?");
        foreach (var valueProperty in filter.ValueProperties)
        {
            stringBuilder.Append($"{valueProperty.Name.ToLower()}={valueProperty.GetValue(filter)}&");
        }

        stringBuilder.Length--;

        return stringBuilder.ToString();
    }
}