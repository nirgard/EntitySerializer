using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

/// <summary>
/// Provides a simple strategy for mapping and unmapping property values during entity serialization.
/// </summary>
public class SimpleMapperStrategy : IMapperStrategy
{
    private readonly IPropertyManager PropertyManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleMapperStrategy"/> class.
    /// </summary>
    /// <param name="propertyManager">The property manager.</param>
    public SimpleMapperStrategy(IPropertyManager propertyManager)
    {
        PropertyManager = propertyManager;
    }

    /// <inheritdoc />
    public object? MapValue(Type type, object? value)
    {
        if (TypeHelper.IsEntity(type))
        {
            CachedEntity<object?> cachedEntity = new(PropertyManager, type);
            object nestedMap = cachedEntity.SerializeToMap(value);

            return nestedMap;
        }

        return value;
    }

    /// <inheritdoc />
    public object? UnmapValue(Type type, object? value)
    {
        if (TypeHelper.IsEntity(type) && value is Dictionary<string, object?> map)
        {
            CachedEntity<object?> cachedEntity = new(PropertyManager, type);
            object? nestedUnmap = cachedEntity.DeserializeFromMap(map);

            return nestedUnmap;
        }

        return value;
    }
}