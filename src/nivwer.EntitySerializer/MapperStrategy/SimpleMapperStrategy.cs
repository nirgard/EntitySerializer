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
            Console.WriteLine($"Entity: {type}");
            CachedEntity<object?> cachedEntity = new CachedEntity<object?>(PropertyMapper, type);
            object nestedMap = cachedEntity.SerializeToMap(value);

            return nestedMap;
        }
        Console.WriteLine($"Simple: {type}");
        return value;
    }
}