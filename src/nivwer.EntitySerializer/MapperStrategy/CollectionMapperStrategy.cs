using System.Collections;
using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer.MapperStrategy;

public class CollectionMapperStrategy : IMapperStrategy
{
    private readonly IMapperStrategy RecursiveMapperStrategy;

    public CollectionMapperStrategy(IMapperStrategy recursiveMapperStrategy)
    {
        RecursiveMapperStrategy = recursiveMapperStrategy;
    }

    public object? MapValue(Type type, object? value)
    {
        Type? elementType = GetCollectionElementType(type);
        IEnumerable? collection = value as IEnumerable;

        if (collection == null || elementType == null)
        {
            return value;
        }

        IList mappedCollection = CreateListInstance(elementType, true);

        foreach (object? item in collection)
        {
            object? mappedItem = RecursiveMapperStrategy.MapValue(elementType, item);
            mappedCollection.Add(mappedItem);
        }

        // Support to arrays
        if (type.IsArray)
        {
            return ConvertToListToArray(elementType, mappedCollection);
        }

        return mappedCollection;
    }

    public object? UnmapValue(Type type, object? value)
    {
        Type? elementType = GetCollectionElementType(type);
        IEnumerable? collection = value as IEnumerable;

        if (collection == null || elementType == null)
        {
            return value;
        }
        
        IList unmappedCollection = CreateListInstance(elementType);

        foreach (object? item in collection)
        {
            object? unmappedItem = RecursiveMapperStrategy.UnmapValue(elementType, item);
            unmappedCollection.Add(unmappedItem);
        }

        // Support to arrays
        if (type.IsArray)
        {
            return ConvertToListToArray(elementType, unmappedCollection);
        }

        return unmappedCollection;
    }

    private IList CreateListInstance(Type elementType, bool toMap = false)
    {
        if (toMap && TypeHelper.IsEntity(elementType))
            elementType = typeof(Dictionary<string, object?>);

        Type listType = typeof(List<>).MakeGenericType(elementType);
        IList collection = (IList)Activator.CreateInstance(listType)!;

        return collection;
    }

    private Array ConvertToListToArray(Type elementType, IList list)
    {
        var array = Array.CreateInstance(elementType, list.Count);
        list.CopyTo(array, 0);

        return array;
    }

    private Type? GetCollectionElementType(Type type)
    {
        Type? elementType = null;

        if (type.IsArray)
            elementType = type.GetElementType();
        else if (type.IsGenericType)
            elementType = type.GetGenericArguments().FirstOrDefault();

        return elementType;
    }
}