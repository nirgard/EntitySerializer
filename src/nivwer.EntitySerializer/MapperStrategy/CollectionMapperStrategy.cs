using System.Collections;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

public class CollectionMapperStrategy : IMapperStrategy
{
    private readonly IPropertyMapper PropertyMapper;

    public CollectionMapperStrategy(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
    }

    public object? MapValue(Type type, object? value)
    {
        Type? elementType = GetCollectionElementType(type);

        if (value is IEnumerable collection && elementType != null)
        {
            Console.WriteLine($"Collection: {type}");
            IList<object?> mappedCollection = [];

            foreach (object? item in collection)
            {
                object? mappedItem = PropertyMapper.MapValue(elementType, item);
                mappedCollection.Add(mappedItem);
            }
            
            return mappedCollection;
        }
        else
        {
            Console.WriteLine($"Collection not IEnumerable: {type}");
            return value;
        }
    }

    private Type? GetCollectionElementType(Type type)
    {
        if (type.IsArray)
        {
            return type.GetElementType();
        }
        else if (type.IsGenericType)
        {
            return type.GetGenericArguments()[0];
        }

        return null;
    }
}