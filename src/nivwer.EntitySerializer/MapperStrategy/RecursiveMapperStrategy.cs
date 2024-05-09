using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

/// <summary>
/// Provides a recursive strategy for mapping and unmapping property values during entity serialization.
/// </summary>
public class RecursiveMapperStrategy : IMapperStrategy
{
    private readonly IPropertyManager PropertyManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecursiveMapperStrategy"/> class.
    /// </summary>
    /// <param name="propertyManager">The property manager.</param>
    public RecursiveMapperStrategy(IPropertyManager propertyManager)
    {
        PropertyManager = propertyManager;
    }

    /// <inheritdoc />
    public object? MapValue(Type type, object? value)
    {
        if (type == typeof(object))
            type = value?.GetType() ?? type;

        IMapperStrategy strategy = GetMapperStrategy(type);
        object? mappedValue = strategy.MapValue(type, value);

        return mappedValue;
    }

    /// <inheritdoc />
    public object? UnmapValue(Type type, object? value)
    {
        if (type == typeof(object))
            type = value?.GetType() ?? type;

        IMapperStrategy strategy = GetMapperStrategy(type);
        object? unmappedValue = strategy.UnmapValue(type, value);

        return unmappedValue;
    }

    private IMapperStrategy GetMapperStrategy(Type type)
    {
        IMapperStrategy strategy;

        if (TypeHelper.IsCollection(type))
            strategy = new CollectionMapperStrategy(this);
        else if (TypeHelper.IsDictionary(type))
            strategy = new DictionaryMapperStrategy(this);
        else
            strategy = new SimpleMapperStrategy(PropertyManager);

        return strategy;
    }
}