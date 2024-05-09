using System.Reflection;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer;

public class PropertyMapper : IPropertyMapper
{
    private readonly IMapperStrategy Strategy;

    public PropertyMapper(IPropertyManager propertyManager, IMapperStrategy? strategy = null)
    {
        Strategy = strategy ?? new RecursiveMapperStrategy(propertyManager);
    }

    public bool UseNestedMapping { get; set; } = false;

    public object? MapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        return UseNestedMapping ? Strategy.MapValue(propertyType, value) : value;
    }

    public object? UnmapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        return UseNestedMapping ? Strategy.UnmapValue(propertyType, value) : value;
    }
}
