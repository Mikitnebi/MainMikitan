using System.Collections;
using System.Reflection;
using AutoMapper.Internal;
using FluentEmail.Core;

namespace MainMikitan.Application.Services.AutoMapper;

public class MapperConfig : IMapperConfig
{
    public TTo Map<TFrom, TTo>(TFrom from, TTo to) 
        where TFrom : class 
        where TTo : class
    {
        var fromType = typeof(TFrom);
        var toType = typeof(TTo);
        
        var fromProperties = GetProperties(fromType);
        var toProperties = GetProperties(toType);
        
        if (fromProperties == null || toProperties == null) return to;
        
        var similarProperties = GetSimilarProperties(fromProperties, toProperties);
        return SetValues(from, to, similarProperties);
    }
    private List<PropertyInfo>? GetProperties(Type data)
    {
        return data.GetProperties()
            .ToList();
    }

    private List<PropertyInfo> GetSimilarProperties(List<PropertyInfo> firstComparable, List<PropertyInfo> secondComparable)
    {
        return firstComparable.Count > secondComparable.Count ? 
            (from firstProperty in firstComparable from secondProperty in secondComparable where firstProperty.Name == secondProperty.Name && firstProperty.GetType() == secondProperty.GetType() select firstProperty).ToList() : 
            (from secondProperty in secondComparable from firstProperty in firstComparable where firstProperty.Name == secondProperty.Name && firstProperty.GetType() == secondProperty.GetType() select firstProperty).ToList();
    }

    private TTo SetValues<TFrom, TTo>(TFrom from, TTo to, List<PropertyInfo> similarProperties)
    {
        var fromProperties = GetProperties(typeof(TFrom));

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