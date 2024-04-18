using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

public class SimpleMapperStrategy : IMapperStrategy
{
    private readonly IPropertyMapper PropertyMapper;

    public SimpleMapperStrategy(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
    }

    public object? MapValue(Type type, object? value)
    {
        if (TypeHelper.IsEntity(type))
        {
            CachedEntity<object?> cachedEntity = new CachedEntity<object?>(PropertyMapper, type);
            object nestedMap = cachedEntity.SerializeToMap(value);

            return nestedMap;
        }

        return value;
    }

    public object? UnmapValue(Type type, object? value)
    {
        if (TypeHelper.IsEntity(type) && value is Dictionary<string, object?> map)
        {
            CachedEntity<object?> cachedEntity = new CachedEntity<object?>(PropertyMapper, type);
            object? nestedUnmap = cachedEntity.DeserializeFromMap(map);

            return nestedUnmap;
        }
        
        return value;
    }
}