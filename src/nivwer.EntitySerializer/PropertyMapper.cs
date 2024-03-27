using System.Collections;
using System.Reflection;
using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class PropertyMapper : IPropertyMapper
{
    public object MapDictionaryToEntity(Type entityType, Dictionary<string, object?> map)
    {
        object? entity = Activator.CreateInstance(entityType);
      
        if (entity == null)
        {
            string message = $"Failed to create an instance of the {entityType.Name} entity.";
            throw new InvalidOperationException(message);
        }

        PropertyInfo[] properties = entityType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (map.TryGetValue(property.Name, out object? value))
            {
                SetValueBasedOnType(entity, property, value);
            }
        }

        return entity;
    }

    private void SetValueBasedOnType<T>(T entity, PropertyInfo property, object? value)
    where T : new()
    {
        if (TypeHelper.IsSimpleType(property.PropertyType))
        {
            SetValueIfSimpleType(entity, property, value);
        }
        else if (TypeHelper.IsCollection(property.PropertyType))
        {
            SetValueIfCollection(entity, property, value);
        }
        else
        {
            // SetValueForNestedObject(entity, property, value);
        }
    }

    private void SetValueIfSimpleType<T>(T entity, PropertyInfo property, object? value)
    {
        property.SetValue(entity, value);
    }

    private void SetValueIfCollection<T>(T entity, PropertyInfo property, object? value)
    {
        if (value is IEnumerable collectionValues)
        {
            Type elementType = property.PropertyType.GetGenericArguments()[0];
            Type listType = typeof(List<>).MakeGenericType(elementType);

            object? collectionObject = Activator.CreateInstance(listType);

            if (collectionObject == null)
            {
                string message = "Failed to deserialize the entity.";
                throw new InvalidOperationException(message);
            }

            IList collection = (IList)collectionObject;

            foreach (var item in collectionValues)
            {
                Dictionary<string, object?> itemDictionary = (Dictionary<string, object?>)item;
                object? elementEntity = MapDictionaryToEntity(elementType, itemDictionary);

                collection.Add(elementEntity);
            }

            property.SetValue(entity, collection);
        }
    }

    // private void SetValueForNestedObject<T>(T entity, PropertyInfo property, object? value) 
    // where T : new()
    // {
    //     object? nestedObject = MapDictionaryToEntity(property.PropertyType, (Dictionary<string, object?>)value);
    //     property.SetValue(entity, nestedObject);
    // }

    public Dictionary<string, object?> MapPropertiesToDictionary<T>(T entity)
    {
        Dictionary<string, object?> map = new();

        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;
            object? propertyValue = property.GetValue(entity);

            map.Add(propertyName, propertyValue);
        }

        return map;
    }
}
