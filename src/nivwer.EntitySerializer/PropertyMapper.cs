using System.Reflection;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer;

/// <summary>
/// Maps property values for entity serialization.
/// </summary>
public class PropertyMapper : IPropertyMapper
{
    private readonly IMapperStrategy Strategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyMapper"/> class.
    /// </summary>
    /// <param name="propertyManager">The property manager.</param>
    /// <param name="strategy">Optional custom mapping strategy.</param>
    public PropertyMapper(IPropertyManager propertyManager, IMapperStrategy? strategy = null)
    {
        Strategy = strategy ?? new RecursiveMapperStrategy(propertyManager);
    }

    /// <inheritdoc />
    public bool UseNestedMapping { get; set; } = false;

    /// <inheritdoc />
    public object? MapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        return UseNestedMapping ? Strategy.MapValue(propertyType, value) : value;
    }

    /// <inheritdoc />
    public object? UnmapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        return UseNestedMapping ? Strategy.UnmapValue(propertyType, value) : value;
    }
}
