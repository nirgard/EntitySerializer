using System.Reflection;
using nivwer.EntityMapSerializer.Helpers;
using nivwer.EntityMapSerializer.Interfaces;

namespace nivwer.EntityMapSerializer;

public class PropertyMapper : IPropertyMapper
{
    public T MapDictionaryToEntity<T>(Dictionary<string, object?> map)
    where T : new()
    {
        var Entity = MapDictionaryToEntity(typeof(T), map);

        if (Entity == null)
        {   
            string message = "Failed to deserialize the entity.";
            throw new InvalidOperationException(message);
        }

        return (T)Entity ?? new T();
    }

    private object? MapDictionaryToEntity(Type entityType, Dictionary<string, object?> map)
    {
        object? entity = Activator.CreateInstance(entityType);
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
            // SetValueIfCollection(entity, property, value);
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
