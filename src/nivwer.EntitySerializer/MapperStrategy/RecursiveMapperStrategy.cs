using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

public class RecursiveMapperStrategy : IMapperStrategy
{
    private readonly IPropertyManager PropertyManager;

    public RecursiveMapperStrategy(IPropertyManager propertyManager)
    {
        PropertyManager = propertyManager;
    }

    public object? MapValue(Type type, object? value)
    {
        if (type == typeof(object))
            type = value?.GetType() ?? type;

        IMapperStrategy strategy = GetMapperStrategy(type);
        object? mappedValue = strategy.MapValue(type, value);

        return mappedValue;
    }

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