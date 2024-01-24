using System.Reflection;

namespace MainMikitan.Application.Services.AutoMapper;

public class MapperConfig : IMapperConfig
{
    public TTo Map<TFrom, TTo>(TFrom from, TTo to) 
        where TFrom : class 
        where TTo : class
    {
        var fromType = from.GetType();
        var toType = to.GetType();
        
        var fromProperties = GetProperties(fromType);
        var toProperties = GetProperties(toType);

        if (fromProperties == null || toProperties == null) return to;
        
        var similarProperties = GetSimilarProperties(fromProperties, toProperties);

        return SetValues(from, to, similarProperties);
    }

    private List<PropertyInfo>? GetProperties<T>(T data)
    {
        return data?
            .GetType()
            .GetProperties()
            .ToList();
    }

    private List<PropertyInfo> GetSimilarProperties(List<PropertyInfo> firstComparable, List<PropertyInfo> secondComparable)
    {
        var similarities = firstComparable.Count > secondComparable.Count ? 
            firstComparable.Intersect(secondComparable).ToList()
            : secondComparable.Intersect(firstComparable).ToList();
        
        return similarities; 
    }

    private TTo SetValues<TFrom, TTo>(TFrom from, TTo to, List<PropertyInfo> similarProperties)
    {
        var fromProperties = GetProperties(from?.GetType());

        if (fromProperties == null) return to;
        var intersectProperties = fromProperties.Intersect(similarProperties).ToList();
        
        foreach (var intersectProperty in intersectProperties)
        {
            var fromPropertyValue = intersectProperty.GetValue(from);
            intersectProperty.SetValue(to, fromPropertyValue);
        }

        return to;
    }
}