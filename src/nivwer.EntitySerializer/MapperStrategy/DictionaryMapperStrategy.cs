using System.Collections;
using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

/// <summary>
/// Provides a strategy for mapping and unmapping dictionary property values during entity serialization.
/// </summary>
public class DictionaryMapperStrategy : IMapperStrategy
{
    private readonly IMapperStrategy RecursiveMapperStrategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="DictionaryMapperStrategy"/> class.
    /// </summary>
    /// <param name="recursiveMapperStrategy">The recursive mapper strategy.</param>    
    public DictionaryMapperStrategy(IMapperStrategy recursiveMapperStrategy)
    {
        RecursiveMapperStrategy = recursiveMapperStrategy;
    }

    /// <inheritdoc />
    public object? MapValue(Type type, object? value)
    {
        Type keyType = GetDictionaryKeyType(type);
        Type valueType = GetDictionaryValueType(type);

        IDictionary? dictionary = value as IDictionary;

        if (dictionary == null)
        {
            return value;
        }

        IDictionary mappedDictionary = CreateDictionaryInstance(keyType, valueType, true);

        foreach (DictionaryEntry entry in dictionary)
        {
            object mappedKey = RecursiveMapperStrategy.MapValue(keyType, entry.Key)!;
            object? mappedValue = RecursiveMapperStrategy.MapValue(valueType, entry.Value);

            mappedDictionary.Add(mappedKey, mappedValue);
        }

        return mappedDictionary;
    }

    /// <inheritdoc />
    public object? UnmapValue(Type type, object? value)
    {
        Type keyType = GetDictionaryKeyType(type);
        Type valueType = GetDictionaryValueType(type);

        IDictionary? dictionary = value as IDictionary;

        if (dictionary == null)
        {
            return value;
        }

        IDictionary unmappedDictionary = CreateDictionaryInstance(keyType, valueType);

        foreach (DictionaryEntry entry in dictionary)
        {
            object mappedKey = RecursiveMapperStrategy.UnmapValue(keyType, entry.Key)!;
            object? mappedValue = RecursiveMapperStrategy.UnmapValue(valueType, entry.Value);

            unmappedDictionary.Add(mappedKey, mappedValue);
        }

        return unmappedDictionary;
    }

    private IDictionary CreateDictionaryInstance(Type keyType, Type valueType, bool toMap = false)
    {
        if (toMap && TypeHelper.IsEntity(keyType))
            keyType = typeof(Dictionary<string, object?>);

        if (toMap && TypeHelper.IsEntity(valueType))
            valueType = typeof(Dictionary<string, object?>);

        Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
        IDictionary dictionary = (IDictionary)Activator.CreateInstance(dictionaryType)!;

        return dictionary;
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