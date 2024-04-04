using System.Collections;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

public class DictionaryMapperStrategy : IMapperStrategy
{
    private readonly IPropertyMapper PropertyMapper;

    public DictionaryMapperStrategy(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
    }

    public object? MapValue(Type type, object? value)
    {
        Type keyType = GetDictionaryKeyType(type);
        Type valueType = GetDictionaryValueType(type);

        if (value is IDictionary dictionary)
        {
            Console.WriteLine($"Dictionary: {type}");
            IDictionary<object, object?> mappedDictionary = new Dictionary<object, object?>();

            foreach (DictionaryEntry entry in dictionary)
            {
                object mappedKey = PropertyMapper.MapValue(keyType, entry.Key)!;
                object? mappedValue = PropertyMapper.MapValue(valueType, entry.Value);

                mappedDictionary.Add(mappedKey, mappedValue);
            }

            return mappedDictionary;
        }
        else
        {
            Console.WriteLine($"Dictionary not IDictionary: {type}");
            return value;
        }
    }

    private Type GetDictionaryKeyType(Type type)
    {
        Type keyType = type.GetGenericArguments()[0];
        return keyType;
    }

    private Type GetDictionaryValueType(Type type)
    {
        Type valueType = type.GetGenericArguments()[1];
        return valueType;
    }
}